using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;
using System.Collections;

public class NetworkLobbyHook : LobbyHook {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
	{
		LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer> ();
		setupLoaclPlayer localPlayer = gamePlayer.GetComponent<setupLoaclPlayer> ();
		localPlayer.pname = lobby.playerName;
		localPlayer.playerColor = lobby.playerColor;
	}
}
