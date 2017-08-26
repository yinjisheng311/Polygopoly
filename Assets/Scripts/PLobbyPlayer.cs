using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This lobby player will be spawned for all users who enter the lobby
public class PLobbyPlayer : NetworkLobbyPlayer {

    //public RectTransform mainPanel; 
    //public RectTransform serverListPanel;

    private int playerIndex;

    public Button kickButton;           // Only visible to host to kick clients
    private Button hostLeaveButton;      // Only visible to host to leave to main panel
    private Button clientLeaveButton;    // Only visible to client to leave to serverlist panel
    public Button readyButton;          // Only Visible to client to mark ready
    private Button startButton;          // Only visible to host to start the game when all players are ready

    public Text playerAssignedName;
    public GameObject readyIcon;

    [SyncVar(hook = "OnNewName")]
    public string playerName = "";

    public void setPlayerIndex(int index)
    {
        playerIndex = index;
    }

    public override void OnClientEnterLobby()
    {
        Debug.Log("Lobby Player entered lobby");
        base.OnClientEnterLobby();

        if(PLobbyManager.lobbySingleton != null)    // This is just to notify lobby manager that the number of clients has increased by 1
        {
            PLobbyManager.lobbySingleton.OnPlayersNumberModified(1);
        }

        PLobbyPlayerList.single_Instance.AddPlayer(this);   // Registers itself to the lobby player list

        hostLeaveButton = PLobbyManager.lobbySingleton.hostLeaveButton;
        clientLeaveButton = PLobbyManager.lobbySingleton.clientLeaveButton;
        startButton = PLobbyManager.lobbySingleton.startButton;

        Debug.Log("Is local player ? : " + isLocalPlayer);
        Debug.Log("Is server? : " + isServer);
        Debug.Log("Is Client ? : " + isClient);

        if (isLocalPlayer)
        {
            SetupLocalPlayer();
        }
        else
        {
            SetupOtherPlayer();
        }

        playerAssignedName.text = "Player " + (playerIndex+1) ;
		Debug.Log (playerAssignedName.text +" HERE");
        playerAssignedName.gameObject.SetActive(true);
    }

    // ------- Call back from sync var
    public void OnNewName(string newName)
    {
        playerName = newName;
        playerAssignedName.text = playerName;
    }
    // ---------------------------

    [Command]
    public void CmdNameChanged(string changedName)
    {
        playerName = changedName;
    }

    [ClientRpc]
    public void RpcLoadingScene()
    {
        SceneManager.LoadScene(6); 
    }

    private void SetupLocalPlayer()
    {
        Debug.Log("Setting up local player");
        Debug.Log("Is local Player a server ? : " + isServer);
        Debug.Log("Is local Player a client ? : " + isClient);

        //have to use child count of player prefab already setup as "this.slot" is not set yet
        //if (playerName == "")
        //    CmdNameChanged("Player" + (PLobbyPlayerList.single_Instance.playerListTransform.childCount - 1));

        if (isServer && isClient)   // This is the host player himself
        {
            startButton.gameObject.SetActive(true);
            startButton.interactable = false;       // Cannot press start button until all players are ready
            Debug.Log("Just set start button to true");

            if(playerIndex == 0)
            {
                hostLeaveButton.gameObject.SetActive(true);
                hostLeaveButton.interactable = true;
                hostLeaveButton.onClick.RemoveAllListeners();
                hostLeaveButton.onClick.AddListener(OnHostLeaveClick);
                Debug.Log("Just set up host leave button");
            }

            if (playerIndex > 0)
            {
                kickButton.gameObject.SetActive(true);
                kickButton.interactable = true;
                kickButton.onClick.RemoveAllListeners();
                kickButton.onClick.AddListener(OnKickButtonPressed);
                Debug.Log("Just set up kick button");
            }
        }
        else        // This is for clients
        {
            readyButton.gameObject.SetActive(true); // Only local player can select ready option
            readyButton.onClick.RemoveAllListeners();   // Makes sure there are no remaining listeners on the ready button
            readyButton.onClick.AddListener(OnReadyClicked);    // Causes OnReadyClick to be called whenever the ready button is pressed.
            Debug.Log("Just set up ready button");

            clientLeaveButton.gameObject.SetActive(true);
            clientLeaveButton.interactable = true;
            clientLeaveButton.onClick.RemoveAllListeners();
            clientLeaveButton.onClick.AddListener(OnClientLeaveClick);
            Debug.Log("Just set up client leave button");
        }
        
    }

    private void SetupOtherPlayer() // If is not local player
    {
        if (isServer)
        {
            Debug.Log("Server in other player cat, breaking out...");
            return;
        }
        Debug.Log("Setting up non-local player...");

        if (playerIndex != 0)
        {
            readyButton.gameObject.SetActive(true); // Only local player can select ready option
            readyButton.onClick.RemoveAllListeners();   // Makes sure there are no remaining listeners on the ready button
            readyButton.onClick.AddListener(OnReadyClicked);    // Causes OnReadyClick to be called whenever the ready button is pressed.
            Debug.Log("Just set up ready button");

            clientLeaveButton.gameObject.SetActive(true);
            clientLeaveButton.interactable = true;
            clientLeaveButton.onClick.RemoveAllListeners();
            clientLeaveButton.onClick.AddListener(OnClientLeaveClick);
            Debug.Log("Just set up client leave button");
        }
    }

    /* This is called after OnStartServer and OnStartClient.
     * When NetworkIdentity.AssignClientAuthority is called on the server, 
     * this will be called on the client that owns the object. 
     * When an object is spawned with NetworkServer.SpawnWithClientAuthority, 
     * this will be called on the client that owns the object.
     * */
    public override void OnStartAuthority()
    {
        Debug.Log("OnStart Authority called, is local player ? : " + isLocalPlayer);
        base.OnStartAuthority();
        SetupLocalPlayer();
        //if (isLocalPlayer)
        //{
        //    Debug.Log("OnStartAuthority called, going to set up local player");
        //    SetupLocalPlayer();
        //}
        //else
        //{
        //    Debug.Log("OnStartAuthority called, going to set up other player");
        //    SetupOtherPlayer();
        //}
        
    }

    public void OnKickButtonPressed()
    {
        if (isLocalPlayer)
        {
            RemovePlayer(); // This player object will be destroyed - on the server and on all clients.
        }
        else if (isServer)
        {
            PLobbyManager.lobbySingleton.KickPlayer(connectionToClient);
        }
    }

    public void OnReadyClicked()
    {
        Debug.Log("Ready button clicked! Sending Ready to server!");
        SendReadyToBeginMessage();
    }

    // This function is called when the a client player calls SendReadyToBeginMessage()
    public override void OnClientReady(bool readyState)
    {
        Debug.Log("OnClientReady called!");
        readyIcon.gameObject.SetActive(readyState);     // This makes the ready icon visible if player is ready, and vice-versa
        readyButton.interactable = !readyState;         // This deactivates ready button if player is ready, and vice-versa
        if (readyState)
        {
            PLobbyManager.lobbySingleton.RegisterReadyPlayer();
        }
        else
        {
            PLobbyManager.lobbySingleton.DeRegisterPlayer();
        }

        if (PLobbyManager.lobbySingleton.checkAllClientsReady())
        {
            EnableStartButton();
        }
    }

    // Method call when start button is clicked, after it is enabled. - Only runs on host
    public void OnStartClicked()
    {
        if (!isServer)
        {
            Debug.Log("You are not the server! Hacked System. THIS WILL BE LOGGED");
            return;
        }
        OnReadyClicked();   // Register itself to network lobby manager as ready
        startButton.interactable = false;
        //TODO: Find out if there is a need to make button inactive
    }

    // To enable the start button for the host
    public void EnableStartButton()
    {
        startButton.interactable = true;                    // Allow start button to be toggled
        startButton.onClick.RemoveAllListeners();           // Remove any remaining listers
        startButton.onClick.AddListener(OnStartClicked);    // And add new method call to button
    }

    public void OnHostLeaveClick()
    {
        OnDestroy();
        PLobbyManager.lobbySingleton.ResetNumPlayers(0);
        PLobbyManager.lobbySingleton.StopHost();
        PLobbyManager.lobbySingleton.ChangeTo(PLobbyManager.lobbySingleton.mainPanel);
        hostLeaveButton.gameObject.SetActive(false);
    }

    public void OnClientLeaveClick()
    {
        OnDestroy();
        PLobbyManager.lobbySingleton.OnPlayersNumberModified(1);
        PLobbyManager.lobbySingleton.StopClient();
        PLobbyManager.lobbySingleton.ChangeTo(PLobbyManager.lobbySingleton.serverListPanel);
        clientLeaveButton.gameObject.SetActive(false);
    }

    [ClientRpc]
    public void RpcUpdateCountdown(int countdown)
    {
        //PLobbyManager.lobbySingleton.countdownPanel.UIText.text = "Match Starting in " + countdown; // Show the countdown
        //PLobbyManager.lobbySingleton.countdownPanel.gameObject.SetActive(countdown != 0);           // And Disable the panel if countdown reaches 0
    }

    // Cleanup lobbyPanel when lobby player gets destroyed - when client gets kicked or disconnects
    public void OnDestroy()
    {
        PLobbyPlayerList.single_Instance.RemovePlayer(this);
        if (PLobbyManager.lobbySingleton != null)
        {
            if (isServer)
            {
                PLobbyManager.lobbySingleton.ResetNumPlayers(0);
            }
            else
            {
                PLobbyManager.lobbySingleton.OnPlayersNumberModified(-1);
            }
        }
    }
}
