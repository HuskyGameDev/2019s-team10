using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour {

    //class to control the master volume with the scroller 
	public void SetVolume(float volume){

        //Wwise function that allows the change of volume
        AkSoundEngine.SetRTPCValue("Main_Volume", volume);
    }
}
