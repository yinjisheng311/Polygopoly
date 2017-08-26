using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkHost : NetworkBehaviour {
	public NetworkManager mngr;
	
	// Use this for initialization
	void Start () {
		
		if(mngr != null)
		{
			mngr.StartHost();
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(SceneManager.GetActiveScene().name != "Level1Ryan")
		//NetworkServer.Reset();
		{
			Debug.Log("Stopping Host");
			mngr.StopHost();
		}
		*/
	}
	
	public void Awake(){
		
        this.mngr = GetComponent<NetworkManager>();
		
        
	}
	/*
	public override void OnClientSceneChanged(NetworkConnection conn)
     {
         string loadedSceneName = SceneManager.GetSceneAt(0).name;
         if (loadedSceneName == lobbyScene)
         {
             if (client.isConnected)
                 CallOnClientEnterLobby();
         }
         else
         {
             CallOnClientExitLobby();
         }
 
         /// This call is commented out since it causes a unet "A connection has already been set as ready. There can only be one." error.
         /// More info: http://answers.unity3d.com/questions/991552/unet-a-connection-has-already-been-set-as-ready-th.html
         //base.OnClientSceneChanged(conn);
         OnLobbyClientSceneChanged(conn);
     }
	 */
}