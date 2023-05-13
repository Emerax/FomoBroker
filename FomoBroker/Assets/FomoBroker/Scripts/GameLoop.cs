using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : NetworkBehaviour {
    [SerializeField]
    private FusionCallbacksAPI fusion;
    [SerializeField]
    private UIVisibility ui;
    [SerializeField]
    private TMP_InputField roomNameInput;
    [SerializeField]
    private Button joinOrHostButton;
    [SerializeField]
    private Button startButton;

    private GameState gameState = GameState.START;

    private void Awake() {
        //UI Callbacks
        joinOrHostButton.onClick.AddListener(JoinOrHostGame);
        //startButton.onClick.AddListener(StartGame);

        //Network callbacks
        fusion.JoinGameEvent += OnJoinGame;

        ui.ShowJoinOrHost();
    }


    private void JoinOrHostGame() {
        Debug.Log("Join or host pressed!");
        fusion.JoinOrHostGame(roomNameInput.text);
    }

    private void StartGame() {
        Debug.Log("Start game pressed!");
    }

    private void OnJoinGame(bool isHost) {
        Debug.Log($"Joined game as {(isHost ? "host" : "client")}");
        ui.ShowLobby();
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
