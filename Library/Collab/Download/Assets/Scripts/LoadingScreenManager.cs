using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour {

	[Header("Loading Visuals")]
	public Image loadingIcon;
	public Image loadingDoneIcon;
	public Text loadingText;
	public Image progressBar;
	public Image fadeOverlay;
	public Text extraInfo;

	[Header("Timing Settings")]
	public float waitOnLoadEnd = 0.25f;
	public float fadeDuration = 0.25f;

	[Header("Loading Settings")]
	public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
	public ThreadPriority loadThreadPriority;

	[Header("Other")]
	// If loading additive, link to the cameras audio listener, to avoid multiple active audio listeners
	public AudioListener audioListener;

	AsyncOperation operation;
	Scene currentScene;

	public static int sceneToLoad = 2;
	// IMPORTANT! This is the build index of your loading scene. You need to change this to match your actual scene index
	static int loadingSceneIndex = 4;

	public static void LoadScene(int levelNum) {				
		Application.backgroundLoadingPriority = ThreadPriority.High;
		sceneToLoad = levelNum;
		SceneManager.LoadScene(loadingSceneIndex);
	}

	void Start() {
		if (sceneToLoad < 0)
			return;
		sceneToLoad = 5;//change to 2:Level1, 5 for Level1S 
		fadeOverlay.gameObject.SetActive(true); // Making sure it's on so that we can crossfade Alpha
		currentScene = SceneManager.GetActiveScene();
        Debug.Log("Starting loading scene coroutine...");
		StartCoroutine(LoadAsync(sceneToLoad));
	}

	private IEnumerator LoadAsync(int levelNum) {
		ShowLoadingVisuals();

		yield return null; 

		FadeIn();
		StartOperation(levelNum);

		//fake loading 
		yield return new WaitForSeconds (2.0f);
        Debug.Log("After waiting for 2 seconds");

		float lastProgress = 0f;

		// operation does not auto-activate scene, so it's stuck at 0.9
		while (DoneLoading() == false) {
			Debug.Log ("STUCK HERE");
			yield return null;


			if (Mathf.Approximately(operation.progress, lastProgress) == false) {
				progressBar.fillAmount = operation.progress;
				lastProgress = operation.progress;
			}
		}
        Debug.Log("Scene has been loaded");
		if (loadSceneMode == LoadSceneMode.Additive)
			audioListener.enabled = false;

		ShowCompletionVisuals();

		yield return new WaitForSeconds(waitOnLoadEnd);

		//set extra info to disappear
		extraInfo.gameObject.SetActive (false);

		FadeOut();

		yield return new WaitForSeconds(fadeDuration);

		if (loadSceneMode == LoadSceneMode.Additive)
			SceneManager.UnloadScene(currentScene.name);
		else
			operation.allowSceneActivation = true;

    }

    private void StartOperation(int levelNum) {




		Application.backgroundLoadingPriority = loadThreadPriority;
		operation = SceneManager.LoadSceneAsync(levelNum, loadSceneMode);


		if (loadSceneMode == LoadSceneMode.Single)
			operation.allowSceneActivation = false;
	}

	private bool DoneLoading() {
		return (loadSceneMode == LoadSceneMode.Additive && operation.isDone) || (loadSceneMode == LoadSceneMode.Single && operation.progress >= 0.9f); 
	}

	void FadeIn() {
		fadeOverlay.CrossFadeAlpha(0, fadeDuration, true);
	}

	void FadeOut() {
		fadeOverlay.CrossFadeAlpha(1, fadeDuration, true);
	}

	void ShowLoadingVisuals() {
		loadingIcon.gameObject.SetActive(true);
		loadingDoneIcon.gameObject.SetActive(false);
		float toBeFilled = Random.Range (0.1f, 0.6f);
		progressBar.fillAmount = toBeFilled;
		string[] listOfExtraInfo = {"Collusion is the act of working\n" + "together to establish a monopoly", "Preserve the monopoly of Polygons!\nCommence Operation Polygopoly!", "Polygon is awesome.\nPolygon is life.", "Zerogons were Polygons in the past but\n they gained too many sides and lost themselves", "The more sides a Polygon gain,\n the more it becomes a Zerogon"};
		int extraToBeChosen = Random.Range (0, listOfExtraInfo.Length-1);

		extraInfo.text = listOfExtraInfo[extraToBeChosen];

		loadingText.text = "L O A D I N G . . .";
	}

	void ShowCompletionVisuals() {
		loadingIcon.gameObject.SetActive(false);
		loadingDoneIcon.gameObject.SetActive(true);


		progressBar.fillAmount = 1f;
		loadingText.text = "D O N E ";
	}

}