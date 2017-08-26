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
        string matchName;
        if ((matchName = match.name).Equals(""))
        {
			//Debug.Log (PLobbyManager.lobbySingleton.matchInfo.networkId.ToString ());
			matchName = "Collusion " + PLobbyManager.lobbySingleton.matchInfo.networkId.ToString();
        }
		matchName = match.networkId.ToString ();
        serverInfoText.text = matchName;
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
		lobbyManager.DisplayIsConnecting ("CLIENT");
	}
    

}
