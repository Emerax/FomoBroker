using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FusionAPI : MonoBehaviour, INetworkRunnerCallbacks {
    public Action<bool> JoinGameEvent;
    public Action<int> PlayerCountChangedEvent;

    private NetworkRunner networkRunner;
    private int playerID;

    public float NetworkDeltaTime => networkRunner.DeltaTime;
    public int PlayerID => playerID;

    private void Awake() {
        networkRunner = GetComponent<NetworkRunner>();
    }

    private void Update() {
    }

    public async void JoinOrHostGame(string roomName) {
        StartGameResult res = await networkRunner.StartGame(new StartGameArgs() {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = roomName,
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
        });

        if(res.Ok) {
            JoinGameEvent.Invoke(networkRunner.IsServer);
            int playerCount = networkRunner.ActivePlayers.Count();
            PlayerCountChangedEvent.Invoke(playerCount);
            playerID = playerCount - 1;
        }
        else {
            Debug.Log($"Error starting: {res.ErrorMessage}");
        }
    }

    public void OnConnectedToServer(NetworkRunner runner) {
        Debug.Log($"Connected, mode is {runner.Mode}");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) {
        Debug.Log("Connect failed");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner) {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input) {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) {
        PlayerCountChangedEvent.Invoke(runner.ActivePlayers.Count());
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {
        PlayerCountChangedEvent.Invoke(runner.ActivePlayers.Count());
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) {
    }

    public void OnSceneLoadDone(NetworkRunner runner) {
    }

    public void OnSceneLoadStart(NetworkRunner runner) {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) {
    }
}
