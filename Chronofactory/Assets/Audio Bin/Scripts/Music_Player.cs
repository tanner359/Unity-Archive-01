using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Player : MonoBehaviour
{

    [FMODUnity.EventRef] //Event Caller
    public string soundInput;
    FMOD.Studio.EventInstance Level_Music;

    static public int musicToggle;


    // Start is called before the first frame update
    void Awake()
    {

        // FMOD Starter
        Level_Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Level Music");
        Level_Music.start();

        if(musicToggle == 0)
        {
            Factory_Theme();
        }
            else if (musicToggle == 1)
            {
                Medieval_Theme();
            }
    }

 void Factory_Theme()
    {
        Debug.Log("Audio.Log: Factory Theme is playing");
        Level_Music.setParameterByName("To Factory", 1); //changes sound parameter
        Level_Music.setParameterByName("To Medieval", 0); //changes sound parameter
        
    }

 void Medieval_Theme()
    {
        Debug.Log("Audio.Log: Medieval Theme is playing");
        Level_Music.setParameterByName("To Factory", 0); //changes sound parameter
        Level_Music.setParameterByName("To Medieval", 1); //changes sound parameter
        
    }
}
