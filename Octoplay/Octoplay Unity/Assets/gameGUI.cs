using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class gameGUI : MonoBehaviour 
{
	public static int[] levelStatus = new int[10];
	public Texture2D[] levelTexture = new Texture2D[10];
	public Texture2D lockedTexture;
	public Texture2D returnTexture;
	public Texture2D okayTexture;
	public Texture2D titleTexture;
	public Texture2D awardTexture;
	public Texture2D starTexture;
	public Texture2D[] bubbleTexture = new Texture2D[2];
	public static bool showMenu = true;
	public static bool showSettings = false;
	public bool showSplash = true;
	public GUISkin octoSkin;
	public GUISkin octoSkin2;
	public GUISkin awardSkin;
	GameObject octopus;
	string playerEntry;
	private TouchScreenKeyboard userKeyboard;
	private GUIStyle roundPromptStyle;
	private GUIStyle newLevelPromptStyle;
	private GUIStyle firstLevelLoadingStyle;
	private bool initializedInitialsEntry = false;
	// Use this for initialization
	void Start () 
	{
		octopus = GameObject.FindGameObjectWithTag ("Octopus");
		refreshMenu ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (showSplash && (Input.GetButtonDown("Note_1") || Input.GetButtonDown("Note_2") ||  Input.GetButtonDown("Note_3") ||  Input.GetButtonDown("Note_4") ||  Input.GetButtonDown("Note_5") ||  Input.GetButtonDown("Note_6") ||  Input.GetButtonDown("Note_7") ||  Input.GetButtonDown("Note_8") || Input.GetMouseButtonDown(0))) 
		{
			showSplash = false;
		}
	}

	void OnGUI()
	{
		GUI.skin = octoSkin;
		if(showSplash)
		{
			GUI.DrawTexture(new Rect(Screen.width/2-452, 50, 904, 243), titleTexture);
		}
		else if(showMenu)
		{
			GUI.DrawTexture(new Rect(Screen.width/2-452, 50, 904, 243), titleTexture);
			for(int i=0; i<10; i++)
			{
				if(levelStatus[i]>0)
				{
					if(GUI.Button(new Rect((Screen.width/2-425)+(Mathf.Floor(i%5)*150+(Mathf.Floor(i%5)*25)), (Screen.height-350)+(Mathf.Floor(i/5)*150+(Mathf.Floor(i/5)*25)), 150, 150), levelTexture[i]))
					{
						octoplayGame.currentRound=(i+1);
						octoplayGame.gameInProgress = true;
						StartCoroutine(octopus.GetComponent<octoplayGame>().runGame());
						showMenu=false;
					}
					if(levelStatus[i]==2)
					{
						GUI.DrawTexture(new Rect((Screen.width/2-374)+(Mathf.Floor(i%5)*150+(Mathf.Floor(i%5)*25)), (Screen.height-245)+(Mathf.Floor(i/5)*150+(Mathf.Floor(i/5)*25)), 48, 42), starTexture);
					}
					else if(levelStatus[i]==3)
					{
						GUI.DrawTexture(new Rect((Screen.width/2-398)+(Mathf.Floor(i%5)*150+(Mathf.Floor(i%5)*25)), (Screen.height-245)+(Mathf.Floor(i/5)*150+(Mathf.Floor(i/5)*25)), 48, 42), starTexture);
						GUI.DrawTexture(new Rect((Screen.width/2-350)+(Mathf.Floor(i%5)*150+(Mathf.Floor(i%5)*25)), (Screen.height-245)+(Mathf.Floor(i/5)*150+(Mathf.Floor(i/5)*25)), 48, 42), starTexture);
					}
					else if(levelStatus[i]==4)
					{
						GUI.DrawTexture(new Rect((Screen.width/2-422)+(Mathf.Floor(i%5)*150+(Mathf.Floor(i%5)*25)), (Screen.height-245)+(Mathf.Floor(i/5)*150+(Mathf.Floor(i/5)*25)), 48, 42), starTexture);
						GUI.DrawTexture(new Rect((Screen.width/2-374)+(Mathf.Floor(i%5)*150+(Mathf.Floor(i%5)*25)), (Screen.height-245)+(Mathf.Floor(i/5)*150+(Mathf.Floor(i/5)*25)), 48, 42), starTexture);
						GUI.DrawTexture(new Rect((Screen.width/2-326)+(Mathf.Floor(i%5)*150+(Mathf.Floor(i%5)*25)), (Screen.height-245)+(Mathf.Floor(i/5)*150+(Mathf.Floor(i/5)*25)), 48, 42), starTexture);
					}
				}
				else
				{
					GUI.DrawTexture(new Rect((Screen.width/2-425)+(Mathf.Floor(i%5)*150+(Mathf.Floor(i%5)*25)), (Screen.height-350)+(Mathf.Floor(i/5)*150+(Mathf.Floor(i/5)*25)), 150, 150), lockedTexture);
				}
			}
		}
		else if(showSettings)
		{
			if (!initializedInitialsEntry)
			{
				playerEntry = playerData.playerName;
				initializedInitialsEntry = true;
			}
			GUI.Label(new Rect(Screen.width/2-300, Screen.height/2-100, 600, 100), "Enter Your Initials");
			GUI.skin=awardSkin;
			playerEntry=GUI.TextField(new Rect(Screen.width/2-200, Screen.height/2, 400, 50), playerEntry);
			if(GUI.Button(new Rect(Screen.width/2-128, Screen.height/2+100, 256, 100), okayTexture) || Input.GetKey(KeyCode.Return) || GUI.Button(new Rect(25, Screen.height-123, 100, 102), returnTexture))
			{
				string initials = Regex.Replace(playerEntry, @"[^a-zA-Z]", "").ToUpper();
				if (initials.Length > 3)
				{
					initials = initials.Substring(0, 3);
				}
				if (initials.Length == 0)
				{
					initials = "AAA";
				}
				playerData.playerName = initials;
				PlayerPrefs.SetString("playerName", playerData.playerName);
				showMenu=true;
				showSettings=false;
				initializedInitialsEntry = false;
			}
		}
		else if(octoplayGame.readyForNewGame)
		{
			GUI.DrawTexture(new Rect(Screen.width/2-452, 50, 904, 243), titleTexture);
			if(GUI.Button(new Rect(25, Screen.height-125, 100, 102), returnTexture))
			{
				showMenu=true;
			}
		}



		if(octoplayGame.checkAward==2)
		{
			GUI.DrawTexture(new Rect(Screen.width/2-256, Screen.height/2, 512, 302), awardTexture);
			GUI.skin=awardSkin;
			GUI.Label(new Rect(Screen.width/2-256, Screen.height/2+10, 512, 302), "UNLOCKED");
			GUI.Label(new Rect(Screen.width/2-200, Screen.height/2+100, 400, 275), octoplayGame.awardText);
		}
		if(octoplayGame.gameInProgress)
		{
			GUI.skin = octoSkin2;
			if (octoplayGame.showFirstLevelLoadingPrompt)
			{
				if (firstLevelLoadingStyle == null)
				{
					firstLevelLoadingStyle = new GUIStyle(GUI.skin.label);
					firstLevelLoadingStyle.alignment = TextAnchor.MiddleCenter;
					firstLevelLoadingStyle.fontSize = 80;
					firstLevelLoadingStyle.wordWrap = true;
				}
				GUI.Label(new Rect(0, Screen.height - 110, Screen.width, 80), "Use number keys to mimic the octopus' pattern on the beat", firstLevelLoadingStyle);
			}
			GUI.Label(new Rect(25, 25, 400, 50), "Level " + octoplayGame.currentRound + " Highscore: " + playerData.playerHighScores[(octoplayGame.currentRound-1)]);
			GUI.Label(new Rect(25, Screen.height-75, 1000, 100), "High Score\n" + playerData.playerName + " " + playerData.playerHighScores[(octoplayGame.currentRound-1)]);
			GUI.Label(new Rect(Screen.width-200, 25, 400, 50), "Score: " + octoplayGame.currentScore);
			if (Time.time < octoplayGame.newLevelPromptUntil)
			{
				if (newLevelPromptStyle == null)
				{
					newLevelPromptStyle = new GUIStyle(GUI.skin.box);
					newLevelPromptStyle.alignment = TextAnchor.MiddleCenter;
					newLevelPromptStyle.fontSize = 24;
					newLevelPromptStyle.fontStyle = FontStyle.Bold;
				}
				GUI.Box(new Rect(Screen.width-420, 70, 360, 60), octoplayGame.newLevelPromptText, newLevelPromptStyle);
			}
			if (octoplayGame.roundPromptVisible)
			{
				if (roundPromptStyle == null)
				{
					roundPromptStyle = new GUIStyle(GUI.skin.label);
					roundPromptStyle.alignment = TextAnchor.MiddleCenter;
					roundPromptStyle.fontSize = 64;
					roundPromptStyle.fontStyle = FontStyle.Bold;
				}
				roundPromptStyle.fontSize = octoplayGame.roundPromptText == "Go" ? 80 : 64;
				GUI.Label(new Rect(0, Screen.height / 2 - 60, Screen.width, 120), octoplayGame.roundPromptText, roundPromptStyle);
			}
			for(int x=1; x<=octoplayGame.meterCount; x++)
			{
				GUI.DrawTexture(new Rect(Screen.width-150, 500-(75*x), 64, 64), bubbleTexture[0]);
			}
			for(int x=octoplayGame.meterCount+1; x<=4; x++)
			{
				GUI.DrawTexture(new Rect(Screen.width-150, 500-(75*x), 64, 64), bubbleTexture[1]);
			}
		}

	}

	public static void refreshMenu()
	{
		levelStatus [0] = 1;
		for(int i=0; i<10; i++)
		{
			if(playerData.playerHighScores[i]>=30)
			{
				levelStatus[i]=4;
			}
			else if(playerData.playerHighScores[i]>=25)
			{
				levelStatus[i]=3;
			}
			else if(playerData.playerHighScores[i]>=15)
			{
				levelStatus[i]=2;
			}

			if(playerData.playerHighScores[i]>=octoplayGame.unlockScoreThreshold && i<9)
			{
				levelStatus[i+1]=1;
			}
		}
	}

	public static void PromptForInitialsEntry()
	{
		showMenu = false;
		showSettings = true;
	}
}
