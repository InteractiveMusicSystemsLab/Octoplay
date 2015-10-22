using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;


public class KeyInstruments : MonoBehaviour
{
// pooling object stuff	

		public static bool playingProgression = false;
		public static bool playingNote = false;
		public static bool userCanPlay = false;
		public static string userInstrument = "Octoplay";


		public static PoolableReference<AudioObject> _audioObject1 = new PoolableReference<AudioObject>();
		public static AudioObject audioObject1 // required to use PoolableReference if pooling is enabled, so audioObject != null checks are correct
        {
            get
            {
                return _audioObject1.Get();
            }
            set
		{
                _audioObject1.Set( value );
            }
        }
	public static PoolableReference<AudioObject> _audioObject2 = new PoolableReference<AudioObject>();
	public static AudioObject audioObject2 // required to use PoolableReference if pooling is enabled, so audioObject != null checks are correct
        {
            get
            {
                return _audioObject2.Get();
            }
            set
		{
                _audioObject2.Set( value );
            }
        }
	
	public static PoolableReference<AudioObject> _audioObject3 = new PoolableReference<AudioObject>();
	public static AudioObject audioObject3 // required to use PoolableReference if pooling is enabled, so audioObject != null checks are correct
        {
            get
            {
                return _audioObject3.Get();
            }
            set
		{
                _audioObject3.Set( value );
            }
        }

	public static PoolableReference<AudioObject> _audioObject4 = new PoolableReference<AudioObject>();
	public static AudioObject audioObject4 // required to use PoolableReference if pooling is enabled, so audioObject != null checks are correct
        {
            get
            {
                return _audioObject4.Get();
            }
            set
		{
                _audioObject4.Set( value );
            }
        }
	
	public static PoolableReference<AudioObject> _audioObject5 = new PoolableReference<AudioObject>();
	public static AudioObject audioObject5 // required to use PoolableReference if pooling is endabled, so audioObject != null checks are correct
        {
            get
            {
                return _audioObject5.Get();
            }
            set
		{
                _audioObject5.Set( value );
            }
        }	

	public static PoolableReference<AudioObject> _audioObject6 = new PoolableReference<AudioObject>();
	public static AudioObject audioObject6 // required to use PoolableReference if pooling is enabled, so audioObject != null checks are correct
        {
            get
            {
                return _audioObject6.Get();
            }
            set
		{
                _audioObject6.Set( value );
            }
        }	

	public static PoolableReference<AudioObject> _audioObject7 = new PoolableReference<AudioObject>();
	public static AudioObject audioObject7 // required to use PoolableReference if pooling is enabled, so audioObject != null checks are correct
        {
            get
            {
                return _audioObject7.Get();
            }
            set
		{
                _audioObject7.Set( value );
            }
        }	

	public static PoolableReference<AudioObject> _audioObject8 = new PoolableReference<AudioObject>();
	public static AudioObject audioObject8 // required to use PoolableReference if pooling is enabled, so audioObject != null checks are correct
        {
            get
            {
                return _audioObject8.Get();
            }
            set
		{
                _audioObject8.Set( value );
            }
        }

	public static IEnumerator performLine(int noteNumber, int noteDuration)
	{
		AudioObject temp = audioObject1;
		float floatingDuration=(float) (noteDuration);
		floatingDuration=floatingDuration/1000;
		if (noteNumber==1)
		{
			temp = AudioController.Play( globals.oneChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;
		}
		else if (noteNumber==2)
		{
			temp = AudioController.Play( globals.twoChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;   
		}
		else if (noteNumber==3)
		{
			temp = AudioController.Play( globals.threeChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;   
		}
		else if (noteNumber==4)
		{
			temp = AudioController.Play( globals.fourChord );;
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;    
		}
		else if (noteNumber==5)
		{
			temp = AudioController.Play( globals.fiveChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;   
		}
		else if (noteNumber==6)
		{
			temp = AudioController.Play( globals.sixChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;  
		}
		else if (noteNumber==7)
		{
			temp = AudioController.Play( globals.sevenChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ; 
		}
		else if(noteNumber==8)
		{
			temp = AudioController.Play( globals.eightChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;
		}

	}
	public static IEnumerator performLine(int noteNumber, int noteDuration, string instrumentName)
	{
		AudioObject temp = audioObject1;
		float floatingDuration=(float) (noteDuration);
		floatingDuration=floatingDuration/1000;
		if (noteNumber==1)
		{
			temp = AudioController.Play( globals.oneChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;
		}
		else if (noteNumber==2)
		{
			temp = AudioController.Play( globals.twoChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;   
		}
		else if (noteNumber==3)
		{
			temp = AudioController.Play( globals.threeChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;   
		}
		else if (noteNumber==4)
		{
			temp = AudioController.Play( globals.fourChord );;
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;    
		}
		else if (noteNumber==5)
		{
			temp = AudioController.Play( globals.fiveChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;   
		}
		else if (noteNumber==6)
		{
			temp = AudioController.Play( globals.sixChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;  
		}
		else if (noteNumber==7)
		{
			temp = AudioController.Play( globals.sevenChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ; 
		}
		else if(noteNumber==8)
		{
			temp = AudioController.Play( globals.eightChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;
		}

	}
	public static IEnumerator performLine(int noteNumber, int noteDuration, int patternNumber, string instrumentName)
	{
		//Debug.Log("Performing...");
		Debug.Log("Switching to " + instrumentName + " " + patternNumber);
		AudioObject temp = audioObject1;
		float floatingDuration=(float) (noteDuration);
		floatingDuration=floatingDuration/1000;
		if (noteNumber==1)
		{
			temp = AudioController.Play( globals.oneChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;
		}
		else if (noteNumber==2)
		{
			temp = AudioController.Play( globals.twoChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;   
		}
		else if (noteNumber==3)
		{
			temp = AudioController.Play( globals.threeChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;   
		}
		else if (noteNumber==4)
		{
			temp = AudioController.Play( globals.fourChord );;
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;    
		}
		else if (noteNumber==5)
		{
			temp = AudioController.Play( globals.fiveChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;   
		}
		else if (noteNumber==6)
		{
			temp = AudioController.Play( globals.sixChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;  
		}
		else if (noteNumber==7)
		{
			temp = AudioController.Play( globals.sevenChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ; 
		}
		else if(noteNumber==8)
		{
			temp = AudioController.Play( globals.eightChord );
			yield return new WaitForSeconds(floatingDuration);
			temp.Stop(globals.fadeTime) ;
		}

	}

}