using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PLobbyCountdownPanel : MonoBehaviour
{
    [Header("Loading Visuals")]
    public Image loadingIcon;
    public Image loadingDoneIcon;
    public Text loadingText;
    public Image progressBar;
    public Image fadeOverlay;
    public Text extraInfo;

    public float fadeDuration = 0.25f;

    private void OnEnable()
    {
        ShowLoadingVisuals();
        StartCoroutine(waitTwoSeconds());
    }

    IEnumerator waitTwoSeconds()
    {
        yield return new WaitForSeconds(5.0f);
        ShowCompletionVisuals();
    }

    void FadeIn()
    {
        fadeOverlay.CrossFadeAlpha(0, fadeDuration, true);
    }

    void FadeOut()
    {
        fadeOverlay.CrossFadeAlpha(1, fadeDuration, true);
    }

    void ShowLoadingVisuals()
    {
        loadingIcon.gameObject.SetActive(true);
        loadingDoneIcon.gameObject.SetActive(false);
        float toBeFilled = Random.Range(0.1f, 0.6f);
        progressBar.fillAmount = toBeFilled;
        string[] listOfExtraInfo = { "Collusion is the act of working\n" + "together to establish a monopoly", "Preserve the monopoly of Polygons!\nCommence Operation Polygopoly!", "Polygon is awesome.\nPolygon is life.", "Zerogons were Polygons in the past but\n they gained too many sides and lost themselves", "The more sides a Polygon gain,\n the more it becomes a Zerogon" };
        int extraToBeChosen = Random.Range(0, listOfExtraInfo.Length - 1);

        extraInfo.text = listOfExtraInfo[extraToBeChosen];

        loadingText.text = "L O A D I N G . . .";
    }

    void ShowCompletionVisuals()
    {
        loadingIcon.gameObject.SetActive(false);
        loadingDoneIcon.gameObject.SetActive(true);


        progressBar.fillAmount = 1f;
        loadingText.text = "D O N E ";
        PLobbyManager.lobbySingleton.GoToPlayScene();
    }
}