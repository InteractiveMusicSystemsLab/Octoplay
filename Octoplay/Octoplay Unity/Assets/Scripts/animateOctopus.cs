using UnityEngine;
using System.Collections;

public class animateOctopus : MonoBehaviour {
	public static GameObject octopusObject;
	// Use this for initialization
	void Start () {
		octopusObject = GameObject.FindGameObjectWithTag ("Octopus");
	
	}
	
	public static void moveOctopus(int noteNumber)
	{
			octopusObject.GetComponent<Animation>().Play("leg"+noteNumber);
	}
}
