using UnityEngine;

public class UIVisibility : MonoBehaviour {
    [SerializeField]
    private GameObject joinOrHostRoot;
    [SerializeField]
    private GameObject lobbyRoot;

    public void ShowJoinOrHost() {
        joinOrHostRoot.SetActive(true);
        lobbyRoot.SetActive(false);
    }

    internal void ShowLobby() {
        joinOrHostRoot.SetActive(false);
        lobbyRoot.SetActive(true);
    }
}
