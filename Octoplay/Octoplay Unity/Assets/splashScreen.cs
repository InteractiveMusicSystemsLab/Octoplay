using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class splashScreen : MonoBehaviour 
{
	public Texture2D mainLogo;
	public Texture2D secondLogo;
	public string nextSceneName = "Octoplay";
	public float firstLogoSeconds = 2.0f;
	public float secondLogoSeconds = 2.0f;
	float currentTime;
	int currentSlide = 0;
	bool isLoading = false;
	// Use this for initialization
	void Start () 
	{
		currentTime = Time.time;
		LogBuildScenes();
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool skip = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space);
		if((currentSlide == 0 && Time.time>=currentTime+firstLogoSeconds) || (currentSlide==0 && skip))
		{
			currentTime=Time.time;
			currentSlide=1;
		}
		if(!isLoading && ((currentSlide == 1 && Time.time>=currentTime+secondLogoSeconds) || (currentSlide==1 && skip)))
		{
			isLoading = true;
			if (IsSceneInBuild(nextSceneName))
			{
				SceneManager.LoadScene(nextSceneName);
			}
			else
			{
				Debug.LogError("Scene not found in Build Settings: " + nextSceneName);
				isLoading = false;
			}
		}
	}

	bool IsSceneInBuild(string sceneName)
	{
		for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
		{
			string path = SceneUtility.GetScenePathByBuildIndex(i);
			string name = System.IO.Path.GetFileNameWithoutExtension(path);
			if (name == sceneName)
			{
				return true;
			}
		}
		return false;
	}

	void LogBuildScenes()
	{
		Debug.Log("Splash loading next scene: " + nextSceneName);
		for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
		{
			string path = SceneUtility.GetScenePathByBuildIndex(i);
			string name = System.IO.Path.GetFileNameWithoutExtension(path);
			Debug.Log("Build scene [" + i + "]: " + name + " (" + path + ")");
		}
	}
	void OnGUI()
	{
		if(currentSlide==0)
		{
			GUI.DrawTexture(new Rect(Screen.width/2-199, Screen.height/2-159, 397, 317), mainLogo); 
		}
		else
		{
			GUI.DrawTexture(new Rect(Screen.width/2-193, Screen.height/2-55, 386, 111), secondLogo); 
		}
	}
}
