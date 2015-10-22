using UnityEngine;
using System.Collections;

public class globals : MonoBehaviour 
{
	public static float fadeTime = 0.5F;
	public static Color maintextcolor = new Color32(218,204, 224, 255);
	public static float durationTime = 1.0f;
	public static float patternLoopTime = 1.0f;
	// controls	
	public static string CONTROLS = ("invisible"); // onscreen instrument controls
	
	//samples 
	public static string oneChord = ("Octoplay_C");
	public static string twoChord = ("Octoplay_D");
	public static string threeChord = ("Octoplay_E");
	public static string fourChord = ("Octoplay_F");
	public static string fiveChord = ("Octoplay_G");
	public static string sixChord = ("Octoplay_A");
	public static string sevenChord = ("Octoplay_B");
	public static string eightChord = ("Octoplay_C_2");

	
	
	// instruments
	public static string INSTRUMENT = ("Octoplay"); // which instrument is being used -- sets switch statement in InstrumentSwitcher script
	public static string KEY = ("chromatic"); // not used now, but can be used to allow different key swapping for more sophisticated instruments
	public static bool PATTERN = false; // not used now, but can be used to allow different rhythmic pattern swapping
	public static string ALTCHORDS = ("off"); // not used now, but can be used to allow different chords swapping like inversions
	public static int PATTERNNUMBER = 0;
	
/*
	Controls for Switching Instruments
	
// Resets instruments to blank
		
		resetInstruments.Reset(); 

// Load new instrument with default sound setting 			

	//load instrument
		globals.INSTRUMENT = "piano"; 
	//// load default sound set standard, pattern, scale, scale_8va
		piano.standard_C(); 
*/
}
