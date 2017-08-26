using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SubmarineHealth : NetworkBehaviour {

	[SyncVar]public float maxHealth = 100f;  // Synced to prevent cheating! Maybe? 
	[SyncVar (hook="SetHealthBar")]public float currentHealth = 0f;  // The hook means that the method declared in the hook will be called everytime this variable is changed
	Rigidbody2D myBody;
    Text endGameText;
    public RectTransform healthBar;
	public GameObject shattered_pieces;


    // Use this for initialization
    void Start () {
		Debug.Log ("Submarine Health start() called");
		currentHealth = maxHealth;
		//InvokeRepeating ("DecreaseHealth", 1f, 1f);

	}

    // Decrease Health by default value of 10
	public void DecreaseHealth(){
		//Debug.Log ("Decrease Health has been called");
        // Damage can only be calculated by the server, preventing hacked client from cheating
        // Also, if player is dead, no need to run this code anymore
        //Debug.Log("Decrease Health Method is called! current health is : "+currentHealth);
        if (!isServer || currentHealth <= 0)
        {
            return;
        
		}
		if (isServer){
		Debug.Log("DecreaseHealth() Method called and is server!");

		currentHealth -= 10f; // Should have no need to make explicit call to set player health as the current health variable is already hooked to setHealthBar method
		}
        if(currentHealth <= 0f)
        {
			Rpc_shatter();
            // Call a method on all instances of this object on all clients (RPC)
            Debug.Log("Successful");
			//RpcDied();
			RpcDied();
			//Invoke("RpcDied",0.75f);
			//NetworkServer.Destroy (gameObject);
			//Error: Did not find target for RPC message for 16

            //Invoke("BackToLobby", 3f); // Return back to lobby since player has died. - Probably would want to include some logic here to prevent game from terminating from 1 player dying alone
            return;
			
        }
	}
	[ClientRpc]
    void RpcDied()		//private
    {

		Debug.Log("Changing to End Panel");

		Scene current = this.gameObject.scene;
		Debug.Log(current);
		GameObject[] scenes = current.GetRootGameObjects();
		foreach(GameObject root in scenes)
		{
			Debug.Log(root);
			if(root.GetComponent<RectTransform>() != null)
			{

				Debug.Log("found End");
				GameObject endScene = GameObject.FindGameObjectWithTag("End");
				for(int i = 0; i < endScene.transform.childCount; i++)
				{
					/*
					if (i == 4) {
						if (endScene.transform.GetChild (i).gameObject.transform.position.y != -270) {
							float currentPosition = endScene.transform.GetChild (i).gameObject.transform.position.y;
							Debug.Log (currentPosition);
							float positionToMoveBy = 400 - currentPosition;//digustingly hardcoded way
							endScene.transform.GetChild(i).gameObject.transform.Translate(0, positionToMoveBy, 0);
						}
					}
					*/
					endScene.transform.GetChild(i).gameObject.SetActive(true);
				}
				//Debug.Log(endScene);
				Debug.Log("Finish enabling");
			}

			else if(root.GetComponent<NetworkManager>() != null)
			{
				root.GetComponent<NetworkManager>().StopHost();

			}

			NetworkServer.Reset();
		}
		//NetworkServer.Stop
		//NetworkServer.Shutdown();
		//NetworkServer.DisconnectAll();

		//NetworkServer.Reset();

		//NetworkHost host = gameObject.GetComponent<NetworkHost>();
		//NetworkManager man = host.GetComponent<Network
		//man.StopHost();
		//CmdExit();

        // Find the Text object to display end game message in the scene
        endGameText = GameObject.FindObjectOfType<Text>();
		//endGameText.text = "Game over";
        
		if (isLocalPlayer)//server calling since clientrpc
        {
            //endGameText.text = "Game over";
        }else
        {
            //endGameText.text = "You win!";
        }
        
    }

    private void BackToLobby()
    {
        // Go back to the lobby
        FindObjectOfType<NetworkLobbyManager>().ServerReturnToLobby();
    }

	private void SetHealthBar(float myHealth){

        healthBar.sizeDelta = new Vector2(myHealth/maxHealth*5, healthBar.sizeDelta.y); // Need to normalise size of health bar to the width of the health bar in game

	}

	[ClientRpc]
	void Rpc_shatter(){
		Debug.Log ("Shattering into pieces");
		Debug.Log(gameObject);
		Instantiate (shattered_pieces, gameObject.transform.position, Quaternion.identity);
		Debug.Log("Done shatter");
	}

}
