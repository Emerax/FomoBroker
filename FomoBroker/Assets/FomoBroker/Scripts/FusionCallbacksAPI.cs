using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FusionCallbacksAPI : MonoBehaviour, INetworkRunnerCallbacks {
    public Action JoinGameAsHostEvent;
    public Action JoinGameAsClientEvent;

    private NetworkRunner networkRunner;
    private bool connected;

    private void Awake() {
        networkRunner = GetComponent<NetworkRunner>();
    }

    private void Update() {
        if(!connected) {
            return;
        }
    }

    public async void JoinOrHostGame(string roomName) {
        StartGameResult res = await networkRunner.StartGame(new StartGameArgs() {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = roomName,
        });

        if(res.Ok) {
            Debug.Log($"Start was ok! Mode? {networkRunner.Mode}, game mode: {networkRunner.GameMode}");
            connected = true;
        }
        else {
            Debug.Log($"Error starting: {res.ErrorMessage}");
        }
    }

    public void OnConnectedToServer(NetworkRunner runner) {
        Debug.Log($"Connected, mode is {runner.Mode}");
        connected = true;
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
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {
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
