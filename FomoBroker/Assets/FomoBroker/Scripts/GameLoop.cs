using Fusion;
using UnityEngine;

public class GameLoop : NetworkBehaviour {
    [SerializeField]
    private FusionCallbacksAPI fusion;
    [SerializeField]
    private UI ui;


    private GameState gameState = GameState.START;

    private void Awake() {
        //UI Callbacks
        ui.JoinOrHostButton.onClick.AddListener(JoinOrHostGame);
        //startButton.onClick.AddListener(StartGame);

        //Network callbacks
        fusion.JoinGameEvent += OnJoinGame;
        fusion.PlayerCountChangedEvent += OnPlayerCountChanged;

        EnterState(GameState.START);
    }


    private void JoinOrHostGame() {
        Debug.Log("Join or host pressed!");
        fusion.JoinOrHostGame(ui.RoomNameInput.text);
    }

    private void StartGame() {
        Debug.Log("Start game pressed!");
    }

    private void OnJoinGame(bool isHost) {
        Debug.Log($"Joined game as {(isHost ? "host" : "client")}");
        ui.ShowLobby();
    }

    private void OnPlayerCountChanged(int playerCount) {
        ui.PlayerCountText.text = playerCount.ToString();
    }

    private void ChangeState(GameState newState) {
        ExitState(gameState);
        gameState = newState;
        EnterState(gameState);
    }

    private void ExitState(GameState previousState) {
        switch(previousState) {
            case GameState.START:
                break;
            case GameState.LOBBY:
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
