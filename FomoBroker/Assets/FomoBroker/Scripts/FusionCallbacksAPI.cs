using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FusionCallbacksAPI : MonoBehaviour, INetworkRunnerCallbacks {
    private NetworkRunner networkRunner;
    private bool connected;

    private void Awake() {
        networkRunner = GetComponent<NetworkRunner>();
    }

    private void Start() {
        StartGame(GameMode.AutoHostOrClient);
    }

    private void Update() {
        if(!connected) {
            return;
        }

        if(Input.anyKeyDown) {
            SayHelloRPC();
        }
    }

    public async void StartGame(GameMode mode) {
        Debug.Log("Starting game");

        // Start or join (depends on gamemode) a session with a specific name
        StartGameResult res = await networkRunner.StartGame(new StartGameArgs() {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });

        if(res.Ok) {
            Debug.Log($"Start was ok! Server? {networkRunner.IsServer} Client? {networkRunner.IsClient}");
            connected = true;
        }
        else {
            Debug.Log($"Error starting: {res.ErrorMessage}");
        }
    }

    public void OnConnectedToServer(NetworkRunner runner) {
        Debug.Log("Connected");
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

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void SayHelloRPC(RpcInfo info = default) {
        if(info.Source == networkRunner.LocalPlayer) {
            Debug.Log("You say hi!");
        }
        else {
            Debug.Log($"Other player says hello!");
        }
    }
}
