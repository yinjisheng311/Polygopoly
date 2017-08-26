using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostPolygons : MonoBehaviour {
	public GameObject LobbyPanel;

	public void ChangeToLobby(){

		LobbyPanel.gameObject.SetActive (true);
	}
}
