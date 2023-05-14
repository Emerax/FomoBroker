using Fusion;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory {
    public int money;
    public int[] stocks = new int[3];
}

public class GameLoop : NetworkBehaviour {
    [SerializeField]
    private FusionAPI fusion;
    [SerializeField]
    private UI ui;
    [SerializeField]
    private float actionStateTime = 60;
    [SerializeField]
    private float dividendStateTime = 3;
    [SerializeField]
    private RunManager runManager;
    [SerializeField]
    private List<Temple> temples;
    [SerializeField]
    private Stock stocksVisuals;
    [SerializeField]
    private List<actionableBuilding> buildings;


    [Networked(OnChanged = nameof(ChangeState))]
    private GameState GameState { get; set; } = GameState.START;
    [Networked]
    private float RemainingStateTime { get; set; }

    private bool isHost = false;
    private readonly Dictionary<GameState, IGameStateRunner> stateRunners = new();
    private AttractionManager attractionManager;
    private ActionManager actionManager;

    readonly Dictionary<int, PlayerInventory> inventories = new();

    private int playerCount;

    private void Awake() {
        //Setup runners
        stateRunners[GameState.ACTION] = new ActionStateRunner(actionStateTime);
        stateRunners[GameState.MIGRATION] = new MigrationStateRunner(runManager);
        stateRunners[GameState.DIVIDENDS] = new DividendsStateRunner(dividendStateTime);

        attractionManager = new(temples);
        actionManager = new(buildings);
        

        //UI Callbacks
        ui.JoinOrHostButton.onClick.AddListener(JoinOrHostGame);
        ui.StartButton.onClick.AddListener(StartGame);

        //Network callbacks
        fusion.JoinGameEvent += OnJoinGame;
        fusion.PlayerCountChangedEvent += OnPlayerCountChanged;

        EnterState(GameState.START);
    }

    public override void Spawned() {
        RemainingStateTime = -1;
    }

    public override void FixedUpdateNetwork() {
        if(isHost) {
            if(stateRunners.TryGetValue(GameState, out IGameStateRunner runner)) {
                RemainingStateTime = runner.Run(fusion.NetworkDeltaTime);
                if(runner.IsStateOver(out GameState nextState)) {
                    GameState = nextState;
                }
            }
            else {
                RemainingStateTime = -1;
            }
        }

        if(GameState is GameState.ACTION) {
            ui.UpdateTimer(RemainingStateTime);
        }
    }

    private void TestStockRandomizing() {
        playerCount = 3;
        int[][] psc = RandomizePlayerStocks();
        for(int pi = 0; pi < psc.Length; ++pi) {
            int[] stockCount = psc[pi];
            Debug.Log("Player " + pi + " stock count: " + stockCount[0] + ", " + stockCount[1] + ", " + stockCount[2]);
        }
    }

    private void TestRunnerCountRandomizing() {
        int[] rc = RandomizeRunnerCountForBases();
        Debug.Log("Runner count " + rc[0] + ", " + rc[1] + ", " + rc[2]);
    }

    private void JoinOrHostGame() {
        Debug.Log("Join or host pressed!");
        fusion.JoinOrHostGame(ui.RoomNameInput.text);
    }

    private void StartGame() {
        Debug.Log("Start game pressed!");
        GameState = GameState.ACTION;
    }

    private void OnJoinGame(bool isHost) {
        this.isHost = isHost;
        GameState = GameState.LOBBY;
    }

    private void OnPlayerCountChanged(int playerCount) {
        ui.PlayerCountText.text = playerCount.ToString();
        this.playerCount = playerCount;
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) {
        Debug.Log("Player " + player.PlayerId + " joined");
    }

    private static void ChangeState(Changed<GameLoop> changed) {
        changed.LoadOld();
        GameState prevState = changed.Behaviour.GameState;
        changed.Behaviour.ExitState(prevState);
        changed.LoadNew();
        GameState newState = changed.Behaviour.GameState;
        changed.Behaviour.EnterState(newState);
        Debug.Log($"Changing state from {prevState} to {newState}");
    }

    private uint packStockCountArray(int[] array) {
        return (uint)array[0] | ((uint)array[1] << 8) | ((uint)array[2] << 16);
    }

    private int[] unpackStockCountArray(uint packed) {
        return new int[3] { (int)(packed & 0xff), (int)((packed >> 8) & 0xff), (int)((packed >> 16) & 0xff) };
    }

    int[][] RandomizePlayerStocks() {
        int[][] stockCountForPlayer = new int[playerCount][];
        for(int ii = 0; ii < playerCount; ++ii) {
            stockCountForPlayer[ii] = new int[3];
        }
        if(playerCount == 0) return stockCountForPlayer;


        int[] remainingStocks = new int[3];
        int stockCountPerBase = playerCount * 2;
        int maxStocksForPlayerPerType = stockCountPerBase - 1;

        int remainingTotalStocks = stockCountPerBase * 3;
        for(int ii = 0; ii < 3; ++ii) {
            remainingStocks[ii] = stockCountPerBase;
        }

        while(remainingTotalStocks > 0) {
            for(int pi = 0; pi < playerCount; ++pi) {
                if(remainingTotalStocks == 0) break;
                while(true) {
                    int stockIndex = Random.Range(0, 3);
                    if(remainingStocks[stockIndex] > 0) {
                        stockCountForPlayer[pi][stockIndex]++;
                        remainingStocks[stockIndex]--;
                        remainingTotalStocks--;
                        break;
                    }
                }
            }
        }

        for(int pi = 0; pi < playerCount; ++pi) {
            for(int stockIndex = 0; stockIndex < 3; ++stockIndex) {
                int overflow = stockCountForPlayer[pi][stockIndex] - maxStocksForPlayerPerType;
                if(overflow > 0) {
                    for(int p2 = 0; p2 < playerCount; ++p2) {
                        int room = maxStocksForPlayerPerType - stockCountForPlayer[p2][stockIndex];
                        if(room > 0) {
                            int toGive = Mathf.Min(room, overflow);
                            Debug.Log("Player " + pi + " gave " + toGive + " stocks of type " + stockIndex + " to player " + p2);
                            stockCountForPlayer[p2][stockIndex] += toGive;
                            stockCountForPlayer[pi][stockIndex] -= toGive;
                            overflow -= toGive;
                            if(overflow == 0) break;
                        }
                    }
                }
            }
        }
        return stockCountForPlayer;
    }

    private int[] RandomizeRunnerCountForBases() {
        int totalRunnerCount = 100;
        int[] runnerCountForBase = new int[3];
        float random = Random.value;
        runnerCountForBase[0] = Mathf.RoundToInt(random * (float)totalRunnerCount);
        runnerCountForBase[1] = totalRunnerCount - runnerCountForBase[0];
        int takeFrom0 = Mathf.RoundToInt(Random.Range(0.0f, 0.75f) * (float)runnerCountForBase[0]);
        runnerCountForBase[2] = takeFrom0;
        runnerCountForBase[0] -= runnerCountForBase[2];
        int takeFrom1 = Mathf.RoundToInt(Random.Range(0.0f, 0.75f) * (float)runnerCountForBase[1]);
        runnerCountForBase[2] += takeFrom1;
        runnerCountForBase[1] -= takeFrom1;
        return runnerCountForBase;
    }

    private void ExitState(GameState previousState) {
        switch(previousState) {
            case GameState.START:
                break;
            case GameState.LOBBY:
                ui.HideUI();
                if(isHost) {
                    int[] runnerCountForBase = RandomizeRunnerCountForBases();
                    int[][] stockCountForPlayer = RandomizePlayerStocks();
                    InitGameRPC(runnerCountForBase);

                    foreach((int playerId, int i) in fusion.playerIds.Select((p, i) => (p, i))) {
                        inventories[playerId] = new() {
                            money = 100,
                            stocks = stockCountForPlayer[i]
                        };

                        SetStocksRPC(packStockCountArray(stockCountForPlayer[i]), playerId);
                    }

                }
                break;
            case GameState.ACTION:
                ui.UpdateTimer(-1);
                break;
            case GameState.MIGRATION:
                break;
            case GameState.DIVIDENDS:
                break;
            case GameState.RENT:
                break;
            case GameState.TRADING_SELECT:
                break;
            case GameState.TRADING_BID:
                break;
            case GameState.GAME_OVER:
                break;
        }

        if(isHost && stateRunners.TryGetValue(previousState, out IGameStateRunner runner)) {
            runner.Reset();
        }
    }

    private void EnterState(GameState nextState) {
        switch(nextState) {
            case GameState.START:
                ui.ShowJoinOrHost();
                break;
            case GameState.LOBBY:
                ui.ShowLobby();
                ui.StartButton.gameObject.SetActive(isHost);
                break;
            case GameState.ACTION:
                break;
            case GameState.MIGRATION:
                if(isHost) {
                    int[][] shift = runManager.CalculateRunnerShifting(attractionManager.GetAttractionRatios());
                    RunRunnersRPC(shift[0], shift[1], shift[2]);
                }
                break;
            case GameState.DIVIDENDS:
                if(isHost) {
                    foreach(int playerId in inventories.Keys) {
                        PlayerInventory inv = inventories[playerId];
                        for(int ii = 0; ii < 3; ++ii) {
                            inv.money += inv.stocks[ii] * runManager.runnerCountAtBase[ii];
                            Debug.Log($"Stocks: {inv.stocks[ii]}, runners: {runManager.runnerCountAtBase[ii]}");
                        }
                        ChangeMoneyRPC(inv.money, playerId);
                        Debug.Log("Send Player" + playerId + " has " + inv.money + " money");
                    }
                }
                break;
            case GameState.RENT:
                break;
            case GameState.TRADING_SELECT:
                break;
            case GameState.TRADING_BID:
                break;
            case GameState.GAME_OVER:
                break;
        }
    }

    [Rpc]
    void RunRunnersRPC(int[] shift0, int[] shift1, int[] shift2) {
        runManager.Run(new int[3][] { shift0, shift1, shift2 });
    }

    [Rpc]
    void InitGameRPC(int[] runnerCountForBase) {
        runManager.SpawnDudes(runnerCountForBase);
        if(!inventories.ContainsKey(fusion.PlayerID)) {
            inventories[fusion.PlayerID] = new PlayerInventory();
        }
    }

    [Rpc]
    void ChangeMoneyRPC(int moneyChange, int playerID) {
        if(inventories.TryGetValue(playerID, out PlayerInventory inventory)) {
            inventory.money = moneyChange;
            Debug.Log("Player" + playerID + " has " + inventory.money + " money");
        }
        if(playerID == fusion.PlayerID) {
            ui.UpdateMoney(moneyChange);
        }
    }

    [Rpc]
    private void SetStocksRPC(uint packedStocks, int playerID) {
        if(inventories.TryGetValue(playerID, out PlayerInventory inventory)) {
            int[] stocks = unpackStockCountArray(packedStocks);
            inventory.stocks = stocks;
            stocksVisuals.UpdateVisuals(stocks);
        }
    }
}
