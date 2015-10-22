using UnityEngine;
using System.Collections;

public class resetInstruments : MonoBehaviour 
{
	public static void Reset () 
	{
		globals.oneChord = ("blank");
		globals.twoChord = ("blank");
		globals.threeChord = ("blank");
		globals.fourChord = ("blank");
		globals.fiveChord = ("blank");
		globals.sixChord = ("blank");
		globals.sevenChord = ("blank");
		globals.eightChord = ("blank");	
		globals.fadeTime = 0.5F;
		globals.durationTime = 1.0F;
		globals.PATTERN=false;
		print ("Instrument RESET: default values loaded");
	}	
}

// call the function below before changing timbres to zero out old samples
// resetInstruments.Reset();