using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PInfoPanel : MonoBehaviour {

    private string[] connectingProgress = {"C O N N E C T I N G",
                                           "C O N N E C T I N G .",
                                           "C O N N E C T I N G . .",
                                           "C O N N E C T I N G . . .",};
    public Button cancelButton;
    public Text infoText;
    private int index;
    private string callerStatus;
    public RectTransform infoPanel;

    public void Display(string caller)
    {
        callerStatus = caller;
        cancelButton.interactable = true;
        StartCoroutine(ConnectingProgressCoroutine());
        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(OnCancel);
    }

    public void OnCancel()
    {
        cancelButton.interactable = false;
        if(callerStatus.Equals("HOST") )
        {
            PLobbyManager.lobbySingleton.StopHost();
        }else if(callerStatus.Equals("CLIENT"))
        {
            PLobbyManager.lobbySingleton.StopClient();
        }else
        {
            Debug.Log("UNRECOGNISED CALLER, CHECK ARGUMENT PASSED TO DISPLAY");
        }

        infoPanel.gameObject.SetActive(false);
    }

    public IEnumerator ConnectingProgressCoroutine()
    {
        index = 0;
        infoText.text = connectingProgress[index];
        while (infoPanel.gameObject.activeSelf)
        {
            yield return new WaitForSeconds(1.0f);
            index =  (index + 1) % connectingProgress.Length;
            Debug.Log("Showing index : " + index);
            infoText.text = connectingProgress[index];
        }
    }
}
