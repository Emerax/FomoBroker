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
    private TextMeshProUGUI roomNameText;
    [SerializeField]
    private TextMeshProUGUI playerCountText;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private Clock clock;
    [SerializeField]
    private TextMeshProUGUI moneyText;
    [SerializeField]
    private GameObject gameOverRoot;
    [SerializeField]
    private TextMeshProUGUI phaseText;

    public Transform bidPanel;
    public TextMeshProUGUI bidPriceText;
    public TextMeshProUGUI bidTimerText;
    public TextMeshProUGUI sellerNameText;
    public TextMeshProUGUI highestBidderNameText;
    public Button bidMoreButton;

    public TMP_InputField RoomNameInput { get => roomNameInput; }
    public GameObject JoinOrHostRoot { get => joinOrHostRoot; }
    public Button JoinOrHostButton { get => joinOrHostButton; }
    public GameObject LobbyRoot { get => lobbyRoot; }
    public Button StartButton { get => startButton; }
    public TextMeshProUGUI PlayerCountText { get => playerCountText; }
    public TextMeshProUGUI TimerText { get => timerText; }
    public GameObject GameOverRoot { get => gameOverRoot; }

    void Start() {
        bidPanel.gameObject.SetActive(false);
    }

    public void ShowJoinOrHost() {
        JoinOrHostRoot.SetActive(true);
        LobbyRoot.SetActive(false);
        GameOverRoot.SetActive(false);
    }

    public void ShowLobby() {
        JoinOrHostRoot.SetActive(false);
        
        LobbyRoot.SetActive(true);
        roomNameText.text = "Room: "+roomNameInput.text;
        GameOverRoot.SetActive(false);
        
    }

    public void ShowGameOver() {
        JoinOrHostRoot.SetActive(false);
        LobbyRoot.SetActive(false);
        GameOverRoot.SetActive(true);
    }

    public void HideUI() {
        JoinOrHostRoot.SetActive(false);
        LobbyRoot.SetActive(false);
        GameOverRoot.SetActive(false);
    }

    public void BidMoreButtonPressed() {

    }

    public void SetBidPrice(int cost) {
        bidPriceText.text = $"${cost}";
    }

    public void SetBidTimer(float remaining) {
        bidTimerText.text = remaining.ToString();
    }

    public void UpdateMoney(int money) {
        if(money < 0) {
            moneyText.color = new Color(1.0f, 0.0f, 0.0f);
        }
        else {
            moneyText.color = new Color(0.67f, 0.81f, 0.61f);
        }
        moneyText.text = $"${money}";
    }

    public void UpdateTimer(float time) {
        TimerText.gameObject.SetActive(false);
        //TimerText.gameObject.SetActive(time >= 0);
        //TimerText.text = TimeSpan.FromSeconds(time).ToString("mm':'ss");
        clock.SetTime(time, 10.0f);
    }

    public void OpenStockBidding() {
        bidPanel.gameObject.SetActive(true);
    }

    public void CloseStockBidding() {
        bidPanel.gameObject.SetActive(false);
    }

    public void UpdatePhaseText(string newText) {
        phaseText.text = newText;
    }
}
