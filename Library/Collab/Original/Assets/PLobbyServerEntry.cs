using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;

public class PLobbyServerEntry : MonoBehaviour {

	public Text serverInfoText;
	public Button joinButton;

	public void Populate(MatchInfoSnapshot match, PLobbyManager lobbyManager, Color c)
	{
		serverInfoText.text = match.name;
		NetworkID networkID = match.networkId;

		joinButton.onClick.RemoveAllListeners ();
		joinButton.onClick.AddListener (() => {
			JoinMatch (networkID, lobbyManager);
		});

		GetComponent<Image> ().color = c;

	}

	void JoinMatch(NetworkID networkID, PLobbyManager lobbyManager)
	{
		lobbyManager.matchMaker.JoinMatch (networkID, "", "", "", 0, 0, lobbyManager.OnMatchJoined);
		lobbyManager._isMatchMaking = true;
	}


}
