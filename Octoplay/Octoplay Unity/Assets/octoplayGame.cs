using UnityEngine;
using System.Collections;
public class octoplayGame : MonoBehaviour 
{
	public const int unlockScoreThreshold = 10;
	public static int currentRound = 1;
	public static int currentScore = 0;
	public static bool gameInProgress = false;
	public static bool octopusTurn = true;
	public static string awardText = "";
	public static bool roundPromptVisible = false;
	public static string roundPromptText = "";
	public static float newLevelPromptUntil = 0.0f;
	public static string newLevelPromptText = "New Level Unlocked!";
	private static bool newLevelPromptShown = false;
	private int currentLength = 0;
	private string roundPattern;
	private string userPattern;
	private int lastNote = 0;
	private string[] notes;
	public Transform frontView;
	public Transform overheadView;
	public GameObject treasureChests;
	public ParticleSystem bubbles;
	public static int meterCount = 0;
	AudioObject introTheme;
	AudioObject soundEffects;
	AudioObject soundEffects2;
	bool hotSpot = false;
	bool preloadOctopus = false;
	float timeRemaining;
	public AudioClip[] clickTrackClip = new AudioClip[4];
	AudioSource[] clickTrack = new AudioSource[2];
	public AudioClip[] mainThemeTrack = new AudioClip[2];
	AudioSource[] mainTheme = new AudioSource[2];
	double nextEventTime;
	double nextYieldTime;
	double nextThemeTime;
	double nextMeterCount;
	int flip = 0;



	float intervalWait;
	bool userPlayed=false;
	public static int checkAward = 1;
	public static bool readyForNewGame = true;

	// Use this for initialization
	void Start ()
	{
		if(currentRound<8)
		   intervalWait = 0.3500f;
		else if(currentRound==8)
			intervalWait = 0.2954f;
		else if(currentRound==9)
			intervalWait = 0.2500f;
		else if(currentRound==10)
			intervalWait = 0.2115f;
		int i = 0;
		while (i < 2) {
			GameObject child = new GameObject("Clicktrack");
			child.transform.parent = gameObject.transform;
			clickTrack[i] = child.AddComponent<AudioSource>();
			i++;
		}
		i = 0;
		while (i < 2) {
			GameObject child = new GameObject("Maintheme");
			child.transform.parent = gameObject.transform;
			mainTheme[i] = child.AddComponent<AudioSource>();
			i++;
		}
		clickTrack [0].clip = clickTrackClip[0];
		clickTrack [1].clip = clickTrackClip[0];
		mainTheme [0].clip = mainThemeTrack [0];
		mainTheme [1].clip = mainThemeTrack [1];
		StartCoroutine(queueTheme ());
	}

	void setClickTrack()
	{
		if(currentRound<8)
		{
			intervalWait = 0.3500f;
			clickTrack[0].clip = clickTrackClip[0];
			clickTrack[1].clip = clickTrackClip[0];
		}
		else if(currentRound==8)
		{
			intervalWait = 0.2954f;
			clickTrack[0].clip = clickTrackClip[1];
			clickTrack[1].clip = clickTrackClip[1];
		}
		else if(currentRound==9)
		{
			intervalWait = 0.2500f;
			clickTrack[0].clip = clickTrackClip[2];
			clickTrack[1].clip = clickTrackClip[2];
		}
		else if(currentRound==10)
		{
			intervalWait = 0.2115f;
			clickTrack[0].clip = clickTrackClip[3];
			clickTrack[1].clip = clickTrackClip[3];
		}
	}

	IEnumerator queueTheme()
	{
		flip = 0;
		this.gameObject.GetComponent<Animation>().Play("swim");
		nextThemeTime = AudioSettings.dspTime;
		mainTheme[flip].PlayScheduled(nextThemeTime);
		nextThemeTime += 48.136f;
		flip = 1 - flip;
		while (!gameInProgress)
		{
			if(gameInProgress)
				break;
			while(AudioSettings.dspTime<nextThemeTime-1.0f && !gameInProgress)
			{
				if(gameInProgress)
					break;

				yield return null;
			}
			if(gameInProgress)
				break;
			mainTheme[flip].clip=mainThemeTrack[1];
			mainTheme[flip].PlayScheduled(nextThemeTime);
			nextThemeTime += 48.0f;
			flip = 1 - flip;
		}
		mainTheme [0].Stop ();
		mainTheme [1].Stop ();
	}

	// Update is called once per frame
	void Update ()
	{
		if(!gameInProgress && (Input.GetButtonDown("Note_1") || Input.GetButtonDown("Note_2") ||  Input.GetButtonDown("Note_3") ||  Input.GetButtonDown("Note_4") ||  Input.GetButtonDown("Note_5") ||  Input.GetButtonDown("Note_6") ||  Input.GetButtonDown("Note_7") ||  Input.GetButtonDown("Note_8")) && checkAward==1 && readyForNewGame && !gameGUI.showMenu && !gameGUI.showSettings)
		{
			gameInProgress = true;
			StartCoroutine(runGame ());
		}

		if (!gameInProgress && checkAward == 2 && (Input.GetButtonDown("Note_1") || Input.GetButtonDown("Note_2") ||  Input.GetButtonDown("Note_3") ||  Input.GetButtonDown("Note_4") ||  Input.GetButtonDown("Note_5") ||  Input.GetButtonDown("Note_6") ||  Input.GetButtonDown("Note_7") ||  Input.GetButtonDown("Note_8") || Input.GetMouseButtonDown(0))) 
		{
			checkAward=1;
		}

			if(Input.GetButtonDown("Note_1"))
			{
				receiveNote(1);
			}
			if(Input.GetButtonDown("Note_2"))
			{
				receiveNote(2);
			}
			if(Input.GetButtonDown("Note_3"))
			{
				receiveNote(3);
			}
			if(Input.GetButtonDown("Note_4"))
			{
				receiveNote(4);
			}
			if(Input.GetButtonDown("Note_5"))
			{
				receiveNote(5);
			}
			if(Input.GetButtonDown("Note_6"))
			{
				receiveNote(6);
			}
			if(Input.GetButtonDown("Note_7"))
			{
				receiveNote(7);
			}
			if(Input.GetButtonDown("Note_8"))
			{
				receiveNote(8);
			}
	}
	public IEnumerator showNote(int desiredNote)
	{
		Behaviour h = (Behaviour)GameObject.Find ("Treasure_Chest_Prefab_" + desiredNote).gameObject.GetComponent("Halo");
		h.enabled = true; 
		yield return new WaitForSeconds (0.25f);
		h.enabled = false;
	}
	public IEnumerator showWrongNote(int desiredNote)
	{
		Behaviour h = (Behaviour)GameObject.Find ("Treasure_Chest_Prefab_" + desiredNote).gameObject.transform.GetChild(0).gameObject.GetComponent("Halo");
		h.enabled = true; 
		yield return new WaitForSeconds (2.5f);
		h.enabled = false;
	}
	public void receiveNote(int desiredNote)
	{
		if (octopusTurn || !gameInProgress)
			return;
		if (hotSpot && int.Parse(notes [currentLength]) == desiredNote) 
		{
			userPlayed=true;
			currentLength++;
			StartCoroutine (KeyInstruments.performLine (desiredNote, 600));
			StartCoroutine (showNote(desiredNote));
			userPattern = userPattern + desiredNote + ",";
		}
		else
		{
			octopusTurn=true;
			StartCoroutine (showWrongNote(desiredNote));
			gameInProgress=false;
		}
	}
	void checkUnlock(int roundNumber, int correctNumber)
	{
		if (playerData.playerHighScores[(roundNumber-1)]<unlockScoreThreshold && correctNumber >= unlockScoreThreshold && roundNumber<10) 
		{
			showAward(0);
		} 
		else 
		{
			checkAward = 1;
		}
	}

	void showAward(int awardID)
	{
		awardText = "";
		soundEffects2 = AudioController.Play("Success");
		switch (awardID) 
		{
		case 0:
			checkAward=2;
			awardText="You've unlocked level " + (currentRound+1) + "!";
			newLevelPromptUntil = Time.time + 3.0f;
			//show 2D image
			break;

		case 1:
			Debug.Log ("You unlocked round 3");
			//show 2D image
			break;
		}
	}
	public IEnumerator runGame()
	{
		roundPromptVisible = false;
		roundPromptText = "";
		newLevelPromptShown = false;
		setClickTrack ();
		bubbles.Stop ();
		readyForNewGame = false;
		currentScore = 0;
		StartCoroutine(focusCamera.switchCameras (overheadView));
		introTheme = AudioController.Play ("introTheme");
		yield return new WaitForSeconds (3.6f);
		this.gameObject.GetComponent<Animation>().Play("sitting");
		treasureChests.SetActive (true);
		while (introTheme.audioObjectTime<4.75f) {
			yield return null;
		}
		octopusTurn = true;
		StartCoroutine (runMeterCount());
		StartCoroutine (runClickTrack ());
		StartCoroutine (runOctopus());
		StartCoroutine (runHotspots ());
		while(gameInProgress)
		{
			yield return null;
		}
			clickTrack[0].Stop();
			clickTrack[1].Stop();
			soundEffects = AudioController.Play ("wrongNote");
			yield return new WaitForSeconds(2.0f);
			StartCoroutine(focusCamera.switchCameras (frontView));
			treasureChests.SetActive (false);
			bubbles.Play();
			checkAward=0;
			checkUnlock(currentRound, currentScore);
			while(checkAward!=1)
			{
				yield return null;
			}
			playerData.addHighScore(currentRound, currentScore);
			StartCoroutine(playerData.sendScore(currentRound,currentScore));
			StartCoroutine(queueTheme());
			yield return new WaitForSeconds(1.0f);
			roundPattern = "";
			lastNote = 0;
			StartCoroutine(playerData.getScore());
			readyForNewGame = true;
	}
	IEnumerator runMeterCount()
	{
		meterCount=0;
		nextMeterCount = AudioSettings.dspTime+0.2500f;
		while (gameInProgress)
		{
			if(!gameInProgress)
				break;
			while(AudioSettings.dspTime<nextMeterCount && gameInProgress)
			{
				yield return null;
			}
			if(!gameInProgress)
				break;
			meterCount++;
			if(meterCount>=5)
			meterCount=1;
			nextMeterCount += (intervalWait+0.2500f);
		}		
	}
	IEnumerator runClickTrack()
	{
		nextEventTime = AudioSettings.dspTime+0.2500f;
		clickTrack[flip].PlayScheduled(nextEventTime);
		nextEventTime += (intervalWait+0.2500f)*4;
		flip = 1 - flip;
		while (gameInProgress)
		{
			if(!gameInProgress)
				break;
			while(AudioSettings.dspTime<nextEventTime-1.0f && gameInProgress)
			{
				yield return null;
			}
			if(!gameInProgress)
				break;
			clickTrack[flip].PlayScheduled(nextEventTime);
			nextEventTime += (intervalWait+0.2500f)*4;
			flip = 1 - flip;
		}

	}
	IEnumerator runOctopus()
	{
		//must be called 0.2500f seconds early
		nextYieldTime=AudioSettings.dspTime;
		while (gameInProgress) 
		{
			while (!octopusTurn && gameInProgress)
			{
				yield return null;
			}
			if(!gameInProgress)
				break;
			if (ShouldShowRoundPrompt())
			{
				roundPromptText = "Wait";
				roundPromptVisible = true;
			}
			int x=0;
			generateNewNote();
			notes=roundPattern.Split(',');
			for(int i=0; i<notes.Length-1; i++)
			{
				nextYieldTime+=0.2500f;
				if(!gameInProgress)
					break;
				if(x==4)
				{
					x=0;
				}
				int currentNote = int.Parse(notes[i]);
				animateOctopus.moveOctopus(currentNote);
				while(AudioSettings.dspTime<nextYieldTime)
				{
					yield return null;
				}
				nextYieldTime+=intervalWait;
				StartCoroutine(KeyInstruments.performLine(currentNote, 600));
				StartCoroutine(showNote(currentNote));
				if (ShouldShowRoundPrompt() && notes.Length - 1 == 4 && i == notes.Length - 2)
				{
					float leadSeconds = Mathf.Min(0.6f, (intervalWait + 0.2500f) * 0.9f);
					StartCoroutine(ShowGoPrompt(nextYieldTime, leadSeconds));
				}
				while(AudioSettings.dspTime<nextYieldTime)
				{
					yield return null;
				}
				x++;
			}
			nextYieldTime+=((intervalWait+0.2500f)*(4-x));
			if (ShouldShowRoundPrompt() && notes.Length - 1 != 4)
			{
				StartCoroutine(ShowGoPrompt(nextYieldTime, 0.35f));
			}
			if(!gameInProgress)
				break;
			while(AudioSettings.dspTime<nextYieldTime)
			{
				yield return null;
			}
			octopusTurn = false;
		}
	}
	IEnumerator runHotspots()
	{
		while(gameInProgress)
		{
			while(octopusTurn && gameInProgress)
			{
				yield return null;
			}
			if (ShouldShowRoundPrompt())
			{
				roundPromptVisible = false;
				roundPromptText = "";
			}
			notes=roundPattern.Split(',');
			currentLength=0;
			int x=0;
			for(int i=0; i<notes.Length-1; i++)
			{
				nextYieldTime+=0.050f;
				while(AudioSettings.dspTime<nextYieldTime)
				{
					yield return null;
				}
				nextYieldTime+=0.400f;
				userPlayed=false;
				hotSpot=true;
				while(AudioSettings.dspTime<nextYieldTime)
				{
					yield return null;
				}

				if(!gameInProgress)
					break;
				if(x==4)
				{
					x=0;
				}

				hotSpot=false;
				if(!userPlayed)
				{
					gameInProgress=false;
					break;
				}
				nextYieldTime+=(intervalWait+0.2500f)-0.450;
				while(AudioSettings.dspTime<nextYieldTime)
				{
					yield return null;
				}
				x++;

			}
			nextYieldTime+=((intervalWait+0.2500f)*(4-x));
			if(!gameInProgress)
				break;

			while(AudioSettings.dspTime<nextYieldTime)
			{
				yield return null;
			}
			octopusTurn = true;
			currentScore++;
			TriggerNewLevelPrompt();
		}
	}

	IEnumerator ShowGoPrompt(double downbeatTime, float leadSeconds)
	{
		while (AudioSettings.dspTime < (downbeatTime - leadSeconds) && gameInProgress)
		{
			yield return null;
		}
		if(!gameInProgress)
			yield break;
		roundPromptText = "Go";
		roundPromptVisible = true;
		while (AudioSettings.dspTime < downbeatTime && gameInProgress)
		{
			yield return null;
		}
		roundPromptVisible = false;
		roundPromptText = "";
	}

	private bool ShouldShowRoundPrompt()
	{
		return currentRound == 1 && currentScore <= 10;
	}

	private void TriggerNewLevelPrompt()
	{
		if (newLevelPromptShown)
			return;
		if (currentRound >= 10)
			return;
		if (playerData.playerHighScores[(currentRound - 1)] >= unlockScoreThreshold)
			return;
		if (currentScore < unlockScoreThreshold)
			return;

		newLevelPromptShown = true;
		newLevelPromptUntil = Time.time + 3.0f;
	}
	void generateNewNote()
	{
		if (lastNote == 0)
		{
			lastNote=Random.Range(1, 9);
			roundPattern = ""+lastNote+",";
		}
		else if(currentRound==1)
		{
			int addedNote = Random.Range(-1, 2);
			if(lastNote==1)
				addedNote = Random.Range (0, 2);
			if(lastNote==8)
				addedNote = Random.Range (-1, 1);
			lastNote=lastNote+addedNote;
			roundPattern=roundPattern+lastNote+",";
		}
		else if(currentRound==2)
		{
			int addedNote = Random.Range(-2, 3);
			if(lastNote==1)
				addedNote = Random.Range (0, 3);
			else if(lastNote==2)
				addedNote = Random.Range (-1, 3);
			else if(lastNote==7)
				addedNote = Random.Range (-2, 2);
			else if(lastNote==8)
				addedNote = Random.Range (-2, 1);
			lastNote=lastNote+addedNote;
			roundPattern=roundPattern+lastNote+",";
		}
		else if(currentRound==3)
		{
			int addedNote = Random.Range(-3, 4);
			if(lastNote==1)
				addedNote = Random.Range (0, 4);
			else if(lastNote==2)
				addedNote = Random.Range (-1, 4);
			else if(lastNote==3)
				addedNote = Random.Range (-2, 4);
			else if(lastNote==6)
				addedNote = Random.Range (-3, 3);
			else if(lastNote==7)
				addedNote = Random.Range (-3, 2);
			else if(lastNote==8)
				addedNote = Random.Range (-3, 1);
			lastNote=lastNote+addedNote;
			roundPattern=roundPattern+lastNote+",";
		}
		else if(currentRound==4)
		{
			int addedNote = Random.Range(-4, 5);
			if(lastNote==1)
				addedNote = Random.Range (0, 5);
			else if(lastNote==2)
				addedNote = Random.Range (-1, 5);
			else if(lastNote==3)
				addedNote = Random.Range (-2, 5);
			else if(lastNote==4)
				addedNote = Random.Range (-3, 5);
			else if(lastNote==5)
				addedNote = Random.Range (-4, 4);
			else if(lastNote==6)
				addedNote = Random.Range (-4, 3);
			else if(lastNote==7)
				addedNote = Random.Range (-4, 2);
			else if(lastNote==8)
				addedNote = Random.Range (-4, 1);
			lastNote=lastNote+addedNote;
			roundPattern=roundPattern+lastNote+",";
		}
		else if(currentRound==5)
		{
			int addedNote = Random.Range(-5, 6);
			if(lastNote==1)
				addedNote = Random.Range (0, 6);
			else if(lastNote==2)
				addedNote = Random.Range (-1, 6);
			else if(lastNote==3)
				addedNote = Random.Range (-2, 6);
			else if(lastNote==4)
				addedNote = Random.Range (-3, 5);
			else if(lastNote==5)
				addedNote = Random.Range (-4, 4);
			else if(lastNote==6)
				addedNote = Random.Range (-5, 3);
			else if(lastNote==7)
				addedNote = Random.Range (-5, 2);
			else if(lastNote==8)
				addedNote = Random.Range (-5, 1);
			lastNote=lastNote+addedNote;
			roundPattern=roundPattern+lastNote+",";
		}
		else if(currentRound>=6)
		{
			int addedNote = Random.Range(-6, 7);
			if(lastNote==1)
				addedNote = Random.Range (0, 7);
			else if(lastNote==2)
				addedNote = Random.Range (-1, 7);
			else if(lastNote==3)
				addedNote = Random.Range (-2, 6);
			else if(lastNote==4)
				addedNote = Random.Range (-3, 5);
			else if(lastNote==5)
				addedNote = Random.Range (-4, 4);
			else if(lastNote==6)
				addedNote = Random.Range (-5, 3);
			else if(lastNote==7)
				addedNote = Random.Range (-6, 2);
			else if(lastNote==8)
				addedNote = Random.Range (-6, 1);
			lastNote=lastNote+addedNote;
			roundPattern=roundPattern+lastNote+",";
		}
	}
}
