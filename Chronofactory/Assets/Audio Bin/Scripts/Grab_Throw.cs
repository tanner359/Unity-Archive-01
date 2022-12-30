using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab_Throw : MonoBehaviour
{

    // CODE IS OBSOLETE

/*
    [FMODUnity.EventRef] //Event Caller
    public string soundInput;
    public bool grabObj;
    public bool rotateObj;
    public bool throwObj;


    FMOD.Studio.EventInstance Grab_Drop_Rotate;

    // Start is called before the first frame update
    void Start()
    {
        Grab_Drop_Rotate = FMODUnity.RuntimeManager.CreateInstance("event:/GDR_Event");
        Grab_Drop_Rotate.start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // Sounds_Command();
    }
    
    void Sounds_Command()
    {
        if (rotateObj == true)
        {
            Debug.Log("Audio.Log: Player is rotating");
            Grab_Drop_Rotate.setParameterByName("GDR", 2);
            Grab_Drop_Rotate.release();
            rotateObj = false;

        }
            else if (grabObj == true && !rotateObj && !throwObj)
            {
                Debug.Log("Audio.Log: Player is grabbing");
                Grab_Drop_Rotate.setParameterByName("GDR", 1);
                Grab_Drop_Rotate.start();
                grabObj = false;
        }
            else if (throwObj == true && !rotateObj && !grabObj)
            {
                Debug.Log("Audio.Log: Player is throwing");
                Grab_Drop_Rotate.setParameterByName("GDR", 3);
                Grab_Drop_Rotate.start();
            }

    }
    */
}
