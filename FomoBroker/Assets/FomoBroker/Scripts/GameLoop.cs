using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : NetworkBehaviour {
    [SerializeField]
    private FusionCallbacksAPI fusion;
    [SerializeField]
    private UI ui;
    [SerializeField]
    private float actionStateTime = 60;

    [Networked(OnChanged = nameof(ChangeState))]
    private GameState GameState { get; set; } = GameState.START;
    [Networked]
    private float RemainingStateTime { get; set; }

    private bool isHost = false;
    private readonly Dictionary<GameState, IGameStateRunner> stateRunners = new();

    private void Awake() {
        //Setup runners
        stateRunners[GameState.ACTION] = new ActionStateRunner(actionStateTime);

        //UI Callbacks
        ui.JoinOrHostButton.onClick.AddListener(JoinOrHostGame);
        ui.StartButton.onClick.AddListener(StartGame);

        //Network callbacks
        fusion.JoinGameEvent += OnJoinGame;
        fusion.PlayerCountChangedEvent += OnPlayerCountChanged;

        EnterState(GameState.START);
    }

    public override void FixedUpdateNetwork() {
        if(isHost) {
            if(stateRunners.TryGetValue(GameState, out IGameStateRunner runner)) {
                RemainingStateTime = runner.Run(fusion.NetworkDeltaTime);
                if(runner.IsStateOver(out GameState nextState)) {
                    GameState = nextState;
                }
            }
        }

        ui.UpdateTimer(RemainingStateTime);
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

    private void ExitState(GameState previousState) {
        switch(previousState) {
            case GameState.START:
                break;
            case GameState.LOBBY:
                ui.HideUI();
                break;
            case GameState.ACTION:
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
    }
}
