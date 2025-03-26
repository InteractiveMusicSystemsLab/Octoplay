using UnityEngine;
using System.Collections;

public class splashScreen : MonoBehaviour 
{
	public Texture2D mainLogo;
	public Texture2D secondLogo;
	float currentTime;
	int currentSlide = 0;
	// Use this for initialization
	void Start () 
	{
		currentTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if((currentSlide == 0 && Time.time>=currentTime+8) || (currentSlide==0 && Input.GetMouseButtonDown(0)))
		{
			currentTime=Time.time;
			currentSlide=1;
		}
		if((currentSlide == 1 && Time.time>=currentTime+3) || (currentSlide==1 && Input.GetMouseButtonDown(0)))
		{
			Application.LoadLevel(1);
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
