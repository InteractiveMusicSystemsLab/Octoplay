using UnityEngine;
using System.Collections;

public class userInput : MonoBehaviour {

	private RaycastHit hit;
	private Ray ray;
	
	void Update () 
	{	
		if (Input.GetMouseButtonDown(0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100))
			{
				if(hit.transform.name.Contains("Treasure_Chest_Prefab_"))
				{
					int desiredNote = int.Parse(hit.transform.name.Replace("Treasure_Chest_Prefab_", ""));
					this.gameObject.GetComponent<octoplayGame>().receiveNote(desiredNote);
				}
				if(hit.transform.name.Equals("octopus") && octoplayGame.readyForNewGame && !octoplayGame.gameInProgress && !gameGUI.showMenu && !gameGUI.showSettings)
				{
					octoplayGame.gameInProgress = true;
					StartCoroutine(this.gameObject.GetComponent<octoplayGame>().runGame());
				}

			}
		}
	}


}
