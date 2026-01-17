using UnityEngine;
using System.Collections;

public class playerData : MonoBehaviour 
{
	public static int[] playerHighScores = new int[10];
	public static string[] globalHighScores = new string[10];
	public static string playerName = "AAA";
	private static string passkey="cw7szOco6EnkqhQNhuu67G6sHHc0nHXp1SFoxICraNCqVg10ylWvSQNnnNeyl1Mc";
	void Start()
	{
		playerHighScores = PlayerPrefsX.GetIntArray ("playerHighScores", 0, 10);
		playerName = PlayerPrefs.GetString ("playerName", "AAA");
		UpdateLocalHighScores();
		gameGUI.refreshMenu();
		StartCoroutine(getScore());
	}


	public static void addHighScore(int round, int score)
	{
		round = round - 1;
		bool isNewHighScore = false;
		if(score>playerHighScores[round])
		{
			playerHighScores[round]=score;
			isNewHighScore = true;
		}
		PlayerPrefsX.SetIntArray ("playerHighScores", playerHighScores);
		UpdateLocalHighScores();
		gameGUI.refreshMenu ();
		if (isNewHighScore)
		{
			gameGUI.PromptForInitialsEntry();
		}
	}


	public static IEnumerator sendScore(int round, int score)
	{
		yield break;
	}

	public static IEnumerator getScore()
	{
		UpdateLocalHighScores();
		yield break;
	}
	
	private static void UpdateLocalHighScores()
	{
		for (int i = 0; i < globalHighScores.Length && i < playerHighScores.Length; i++)
		{
			globalHighScores[i] = playerHighScores[i].ToString();
		}
	}
	
	public static string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);
		
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
		
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
		
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
		
		return hashString.PadLeft(32, '0');
	}
}
