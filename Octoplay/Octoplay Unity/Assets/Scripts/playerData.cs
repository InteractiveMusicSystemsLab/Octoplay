using UnityEngine;
using System.Collections;

public class playerData : MonoBehaviour 
{
	public static int[] playerHighScores = new int[10];
	public static string[] globalHighScores = new string[10];
	public static string playerName = "Your Name";
	private static string passkey="cw7szOco6EnkqhQNhuu67G6sHHc0nHXp1SFoxICraNCqVg10ylWvSQNnnNeyl1Mc";
	void Start()
	{
		playerHighScores = PlayerPrefsX.GetIntArray ("playerHighScores", 0, 10);
		playerName = PlayerPrefs.GetString ("playerName", "Your Name");
		gameGUI.refreshMenu();
		StartCoroutine(getScore());
	}


	public static void addHighScore(int round, int score)
	{
		round = round - 1;
		if(score>playerHighScores[round])
		{
			playerHighScores[round]=score;
		}
		PlayerPrefsX.SetIntArray ("playerHighScores", playerHighScores);
		gameGUI.refreshMenu ();
	}

	public static IEnumerator sendScore(int round, int score)
	{
		string hash = Md5Sum(score+passkey+round+playerData.playerName);
		string currentURL = "http://www.knockoutmedia.com/octoplay/send.php?hash="+hash+"&score="+score+"&round="+round+"&username="+WWW.EscapeURL(playerData.playerName);
		
		WWW sendPost = new WWW(currentURL);
		yield return sendPost;
		
		if (sendPost.error != null)
		{

		}
		else
		{
		}
	}

	public static IEnumerator getScore()
	{
		string hash = Md5Sum(passkey+playerData.playerName);
		string currentURL = "http://www.knockoutmedia.com/octoplay/get.php?hash="+hash+"&username="+WWW.EscapeURL(playerData.playerName);

		WWW getPost = new WWW(currentURL);
		yield return getPost;
		
		if (getPost.error != null)
		{

		}
		else
		{
			string[] netScores = getPost.text.ToString().Split(',');
			for(int i=0; i<netScores.Length-1; i++)
			{
				playerData.globalHighScores[i]=netScores[i];
			}
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
