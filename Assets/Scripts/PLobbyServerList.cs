using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class PLobbyServerList : MonoBehaviour {

	public PLobbyManager lobbyManager;

	public RectTransform serverListRect;
	public GameObject serverEntryPrefab;
	public GameObject noServerFound;
    public Button refreshButton;

	protected int currentPage = 0;
	protected int previousPage = 0;

	static Color OddServerColor = new Color(0.0f, 0.0f, 0.0f, 0.5f);
	static Color EvenServerColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    
	void OnEnable()
	{
        lobbyManager.StartMatchMaker();
        currentPage = 0;
		previousPage = 0;

		foreach (Transform t in serverListRect) // This clears previous entries in the list
			Destroy (t.gameObject);

		noServerFound.SetActive (false);
		RequestPage (0);
        refreshButton.onClick.RemoveAllListeners();
        refreshButton.onClick.AddListener(OnRefresh);
	}
    
	public void OnGUIMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		if (matches.Count == 0) {
			if (currentPage == 0) {
				noServerFound.SetActive (true);
			}
			currentPage = previousPage;
			return;
		}

		noServerFound.SetActive (false);
		foreach (Transform t in serverListRect)
			Destroy (t.gameObject);

		for (int i = 0; i < matches.Count; i++) {
			GameObject o = Instantiate (serverEntryPrefab) as GameObject;
			o.GetComponent<PLobbyServerEntry> ().Populate (matches [i], lobbyManager, (i % 2 == 0) ? OddServerColor : EvenServerColor);
			o.transform.SetParent (serverListRect, false);
		}
	
	}
    

	public void ChangePage(int dir)
	{
		int newPage = Mathf.Max (0, currentPage + dir);

		//if we have no server currently displayed, then we need to refresh page0 first instead of trying to fetch
		if (noServerFound.activeSelf)
			newPage = 0;
		RequestPage (newPage);
	}

    public void OnRefresh()
    {
        Debug.Log("Refresh pressed!");
        OnEnable();
        Debug.Log("Finished requesting for page!");
    }


	public void RequestPage(int page)
	{
		previousPage = currentPage;
		currentPage = page;
		Debug.Log ("Lobby manager match maker is : " + lobbyManager.matchMaker);
		lobbyManager.matchMaker.ListMatches (page,              // Starting page number
                                                6,              // how many results in one page
                                                "",             // match name filter
                                                true,           // Remove private matches
                                                0,              // eloscore targets
                                                0,              // Request domain
                                                OnGUIMatchList  // Callback function
                                             );
	}
    

}
