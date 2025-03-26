using UnityEngine;
using System.Collections;

public class demoOctopus : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.D)) {
			StartCoroutine(runDemo ());
		}
	}

	IEnumerator runDemo()
	{
		for (int i=1; i<=8; i++)
		{
			animateOctopus.moveOctopus (i);
			yield return new WaitForSeconds(0.25f);
			StartCoroutine(KeyInstruments.performLine(i, 1000));
			yield return new WaitForSeconds(0.75f);
		}
	}
}
