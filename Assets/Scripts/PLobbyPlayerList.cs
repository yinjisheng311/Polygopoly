using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLobbyPlayerList : MonoBehaviour {

    public static PLobbyPlayerList single_Instance = null;

    public RectTransform playerListTransform;

    public Text matchHash;

    protected VerticalLayoutGroup vLayout;
    protected List<PLobbyPlayer> playersList = new List<PLobbyPlayer>();

	public void OnEnable () {
        single_Instance = this;
        vLayout = playerListTransform.GetComponent<VerticalLayoutGroup>();
		Debug.Log (PLobbyManager.lobbySingleton.matchName);
        matchHash.text = PLobbyManager.lobbySingleton.matchInfo.networkId.ToString();
	}
	
	void Update () {
        // This is to force the layout to recompute every frame to prevent faulty layouts from occuring
        if (vLayout)
        {
            vLayout.childAlignment = Time.frameCount % 2 == 0 ? TextAnchor.UpperCenter : TextAnchor.UpperLeft;
        }
	}

    // This method should be called whenever a new player is added to the lobby
    public void AddPlayer(PLobbyPlayer player)
    {
        if (playersList.Contains(player))   // Adding player that is already in the lobby!
        {
            return;
        }

        playersList.Add(player);
        player.setPlayerIndex(playersList.Count-1);
        //GameObject newPlayerRow = Instantiate(playerRowPrefab) as GameObject;
        //newPlayerRow.transform.SetParent(playerListTransform, false);
        player.transform.SetParent(playerListTransform, false); // false to prevent the world position from changing

    }

    // This method should be called whenever a player leaves the lobby
    public void RemovePlayer(PLobbyPlayer player)
    {
        playersList.Remove(player);
    }

}
