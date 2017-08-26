using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PMainPanel : MonoBehaviour {

    public PLobbyManager lobbyManager;

    public RectTransform PServerListPanel;
    public RectTransform PLobbyPanel;
	public RectTransform PRoomSetupPanel;

    public InputField matchNameInput;
    public InputField matchPasswordInput;

    public Button createButton;
    public Button cancelButton;

    private void Start()
    {
        //PRoomSetupPanel.gameObject.SetActive(false);
    }

    public void OnClickHostGame()
    {
        HostGame(); 
        // TODO: Find out why PRoomSetupPanel is unassigned...
  //      Debug.Log("Host Game Clicked! PRoomSetupPanel is : " + PRoomSetupPanel);
		//PRoomSetupPanel.gameObject.SetActive (true);
  //      matchNameInput.text = "";
  //      //matchPasswordInput.text = "";
  //      //matchPasswordInput.interactable = true;
  //      matchNameInput.interactable = true;
        // TODO: Implement logic to create new sub-panel to ask for match name and password
    }

    public void OnClickCreate()
    {
        matchNameInput.interactable = false;
        //matchPasswordInput.interactable = false;
        HostGame();
    }

    public void OnClickCancel()
    {
        PRoomSetupPanel.gameObject.SetActive(false);
    }

    private void HostGame()
    {
        lobbyManager.DisplayIsConnecting("HOST");
        lobbyManager.StartMatchMaker();
        lobbyManager.matchMaker.CreateMatch(
                    "Collusion " + GetHashCode().ToString(),//matchNameInput.text,                // Match Name
                    (uint)lobbyManager.maxPlayers,      // Number of players
                    true,                               // Advertise Match?
                    "",//matchPasswordInput.text,            // Match Password
                    "",                                 // Public Client Address
                    "",                                 // Private Client Address
                    0,                                  // eloScore for match
                    0,                                  // Request Domain
                    lobbyManager.OnMatchCreate);        // callBack Function

        lobbyManager._isMatchMaking = true;
        //lobbyManager.ChangeTo(PLobbyPanel);
    }

    public void OnClickFindGame()
    {
        lobbyManager.ChangeTo(PServerListPanel);
    }
}
