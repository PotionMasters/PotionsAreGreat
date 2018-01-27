using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

class CustomNetworkManager : NetworkManager
{
    private short connectedPlayers = 0;

    public override void OnStartServer()
    {
        connectedPlayers = 0;
        base.OnStartServer();
        Debug.Log("Server started");
    }
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Player player = Instantiate(playerPrefab).GetComponent<Player>();

        player.id = connectedPlayers;
        Debug.Log("Player " + player.id + " connected");

        NetworkServer.AddPlayerForConnection(conn, player.gameObject, playerControllerId);
        ++connectedPlayers;
    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        Debug.Log("Player disconnected");
        connectedPlayers -= 1;
        base.OnServerDisconnect(conn);
    }
    
}