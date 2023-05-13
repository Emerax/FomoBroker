using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    [SerializeField]
    private GameObject joinOrHostRoot;
    [SerializeField]
    private TMP_InputField roomNameInput;
    [SerializeField]
    private Button joinOrHostButton;
    [SerializeField]
    private GameObject lobbyRoot;
    [SerializeField]
    private TextMeshProUGUI playerCountText;
    [SerializeField]
    private Button startButton;

    public TMP_InputField RoomNameInput { get => roomNameInput; }
    public GameObject JoinOrHostRoot { get => joinOrHostRoot; }
    public Button JoinOrHostButton { get => joinOrHostButton; }
    public GameObject LobbyRoot { get => lobbyRoot; }
    public Button StartButton { get => startButton; }
    public TextMeshProUGUI PlayerCountText { get => playerCountText; }

    public void ShowJoinOrHost() {
        JoinOrHostRoot.SetActive(true);
        LobbyRoot.SetActive(false);
    }

    public void ShowLobby() {
        JoinOrHostRoot.SetActive(false);
        LobbyRoot.SetActive(true);
    }

    public void HideUI() {
        JoinOrHostRoot.SetActive(false);
        LobbyRoot.SetActive(false);
    }
}
