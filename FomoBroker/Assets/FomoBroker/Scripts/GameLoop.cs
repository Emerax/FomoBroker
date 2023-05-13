using Fusion;
using UnityEngine;

public class GameLoop : NetworkBehaviour {
    [SerializeField]
    private FusionCallbacksAPI fusion;
    [SerializeField]
    private UI ui;

    private bool isHost = false;
    [Networked(OnChanged = nameof(ChangeState))]
    private GameState GameState { get; set; } = GameState.START;

    private void Awake() {
        //UI Callbacks
        ui.JoinOrHostButton.onClick.AddListener(JoinOrHostGame);
        ui.StartButton.onClick.AddListener(StartGame);

        //Network callbacks
        fusion.JoinGameEvent += OnJoinGame;
        fusion.PlayerCountChangedEvent += OnPlayerCountChanged;

        EnterState(GameState.START);
    }

    public override void Spawned() {
        base.Spawned();
        Debug.Log($"{name} spawned!");
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
        Debug.Log($"Joined game as {(isHost ? "host" : "client")}");
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
