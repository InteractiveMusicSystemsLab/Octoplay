using UnityEngine;
using System.Collections;

public class octoplay : MonoBehaviour 
{
	public static void standard_C () 
	{
		globals.INSTRUMENT="Octoplay";
		globals.oneChord = ("Octoplay_C");
		globals.twoChord = ("Octoplay_D");
		globals.threeChord = ("Octoplay_E");
		globals.fourChord = ("Octoplay_F");
		globals.fiveChord = ("Octoplay_G");
		globals.sixChord = ("Octoplay_A");
		globals.sevenChord = ("Octoplay_B");
		globals.eightChord = ("Octoplay_C_2");
		globals.fadeTime = 0.5F;
	}	
}
