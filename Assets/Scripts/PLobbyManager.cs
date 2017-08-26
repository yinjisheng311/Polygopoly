using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;


public class PLobbyManager : NetworkLobbyManager {

    static public PLobbyManager lobbySingleton;
    static short MsgKicked = MsgType.Highest + 1;

    protected RectTransform currentPanel;   // Limit access to current panel to this class and children classes
    public RectTransform mainPanel;  // First page that user will see when they first enter multiplayer
    public RectTransform lobbyPanel;
    public RectTransform serverListPanel;
    public RectTransform countDownPanel;
    public PInfoPanel infoPanelScript;

    public Button hostLeaveButton;      // Only visible to host to leave to main panel
    public Button clientLeaveButton;    // Only visible to client to leave to serverlist panel
    public Button readyButton;          // Only Visible to client to mark ready
    public Button startButton;          // Only visible to host to start the game when all players are ready

    public Text matchHash;

    public float prematchCountdown = 5.0f;

    public int _numPlayers = 0;
    public int _numReadyPlayers = 0;

    public bool _isMatchMaking = false;
    protected ulong _currentMatchID;
    protected bool _disconnectServer = false;
    protected bool allClientsReady = false;

    void Start()
    {
        lobbySingleton = this;
        currentPanel = mainPanel;

        GetComponent<Canvas>().enabled = true;
        mainPanel.gameObject.SetActive(true);

        DontDestroyOnLoad(gameObject);      // Makes sure that the lobby manager is not destroyed even when the scene changes

    }

    public void RegisterReadyPlayer()
    {
        _numReadyPlayers ++;
    }

    public void DeRegisterPlayer()
    {
        _numReadyPlayers--;
    }

    public void ResetNumPlayers(int newNum)
    {
        _numPlayers = newNum;
        _numReadyPlayers = newNum;
    }

    public bool checkAllClientsReady()
    {
        if (_numPlayers - _numReadyPlayers == 1)
        {
            return true;
        }else
        {
            return false;
        }
    }

    // Should use this to record any changes in num of players.
    // It uses an input to allow for deduction as well
    public void OnPlayersNumberModified(int change)
    {
        _numPlayers += change;
        if(change < 0)
        {
            DeRegisterPlayer();
        }
    }

    // Method call to change the panel that is currently visible to the player
    public void ChangeTo(RectTransform newPanel)
    {
        if (currentPanel != null)
        {
            currentPanel.gameObject.SetActive(false);
        }

        if (newPanel != null)
        {
            newPanel.gameObject.SetActive(true);
        }

        currentPanel = newPanel;

        if (currentPanel == mainPanel)
        {
            _isMatchMaking = false;
        }
    }

    class KickMsg : MessageBase { }
    public void KickPlayer(NetworkConnection conn)
    {
        conn.Send(MsgKicked, new KickMsg());
    }

    public void KickedMessageHandler(NetworkMessage netMsg)
    {
        netMsg.conn.Disconnect();
    }

    public override void OnStartHost()
    {
        Debug.Log("Host Started!");
        base.OnStartHost();
        infoPanelScript.infoPanel.gameObject.SetActive(false);
        //matchHash.text = lobbySingleton.matchName;//lobbySingleton.GetHashCode().ToString();
//        ChangeTo(lobbyPanel);
    }

    public void DisplayIsConnecting(string caller)
    {
        infoPanelScript.infoPanel.gameObject.SetActive(true);
        infoPanelScript.Display(caller);
        
    }

    public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        base.OnMatchCreate(success, extendedInfo, matchInfo);
        _currentMatchID = (System.UInt64)matchInfo.networkId;
		matchHash.text = _currentMatchID.ToString();
		ChangeTo(lobbyPanel);
    }

    public override void OnDestroyMatch(bool success, string extendedInfo)
    {
        base.OnDestroyMatch(success, extendedInfo);
        if (_disconnectServer)
        {
            StopMatchMaker();
            StopHost();
        }
    }

    // --------------------- Server callbacks ---------------------------

    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject obj = Instantiate(lobbyPlayerPrefab.gameObject) as GameObject;
        return obj;
    }

    // --- Countdown management

    // This method will be called when all players registered on server are ready
    public override void OnLobbyServerPlayersReady()
    {
        Debug.Log("All players ready!");

        Debug.Log("Starting countdown coroutine...");
        //StartCoroutine(ServerCountdownCoroutine());
        ResetNumPlayers(0);     // Going to start the game, reset for future games.

        //ServerChangeScene("LoadingScene");
        //StartCoroutine(pseudoLoading());
        //ServerChangeScene(playScene);
        countDownPanel.gameObject.SetActive(true);
        Debug.Log("Exiting method called when all players are ready!");
    }


    private IEnumerator pseudoLoading()
    {
        yield return new WaitForSeconds(3.0f);
        Debug.Log("3 seconds has passed...");
        ServerChangeScene(playScene);
    }


    // Will initiate a countdown before the game starts, then change scene to play scene after countdown
    public IEnumerator ServerCountdownCoroutine()
    {
        float remainingTime = prematchCountdown;
        int floorTime = Mathf.FloorToInt(remainingTime);

        while (remainingTime > 0)
        {
            yield return null;

            remainingTime -= Time.deltaTime;
            int newFloorTime = Mathf.FloorToInt(remainingTime);

            if (newFloorTime != floorTime)
            {//to avoid flooding the network of message, we only send a notice to client when the number of plain seconds change.
                floorTime = newFloorTime;

                for (int i = 0; i < lobbySlots.Length; ++i)
                {
                    if (lobbySlots[i] != null)
                    {//there is maxPlayer slots, so some could be == null, need to test it before accessing!
                        (lobbySlots[i] as PLobbyPlayer).RpcUpdateCountdown(floorTime);
                    }
                }
            }
        }

        for (int i = 0; i < lobbySlots.Length; ++i)
        {
            if (lobbySlots[i] != null)
            {
                (lobbySlots[i] as PLobbyPlayer).RpcUpdateCountdown(0);
            }
        }

        ServerChangeScene(playScene);
    }

    public void GoToPlayScene()
    {
        Debug.Log("Loading play scene now!");
        ServerChangeScene(playScene);
    }
    
    IEnumerator waitOneSecond()
    {
        yield return new WaitForSeconds(1.0f);
        countDownPanel.gameObject.SetActive(false);
        ChangeTo(null);
    }

    // This is called on the client when the client is finished loading a new networked scene.
    public override void OnLobbyClientSceneChanged(NetworkConnection conn)
    {
        Debug.Log("ENTERED LOBBY CLIENT SCENE CHANGE");
        countDownPanel.gameObject.SetActive(false);
        ChangeTo(null);
    }

    public override void OnLobbyServerSceneChanged(string sceneName)
    {
        Debug.Log("ENTERED LOBBY SERVER SCENE CHANGE");
        countDownPanel.gameObject.SetActive(false);
        ChangeTo(null);
    }

    // ----------------- Client callbacks ------------------

    // Overriden to make clients enter lobby after connecting
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("conn for OnClientConnect is : " + conn);

        conn.RegisterHandler(MsgKicked, KickedMessageHandler);

        if (!NetworkServer.active)
        {//only to do on pure client (not self hosting client)
            infoPanelScript.infoPanel.gameObject.SetActive(false);
            ChangeTo(lobbyPanel);
        }
    }

    // Overriden to make client return to server list after disconnecting
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        ChangeTo(serverListPanel);
    }

    // Overriden to make client return to main page upon error
    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        ChangeTo(mainPanel);
    }

	public void SetMainPanelActive(){
		//new WaitForSeconds (10.0f);
		//mainPanel.gameObject.SetActive (true);
		//Debug.Log("After Waiting 0.5 Seconds");

		StartCoroutine (waitToActivateMainPanel());
	}

	IEnumerator waitToActivateMainPanel(){
		yield return new WaitForSeconds(0.2f);
        Debug.Log("after waiting 0.2 seconds");
		mainPanel.gameObject.SetActive (true);
	}
}
