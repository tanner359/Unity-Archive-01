using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{

    //FMOD Event Manager
    [FMODUnity.EventRef] //Event Caller
    public string soundInput;
    FMOD.Studio.EventInstance Fire_SFX;

    void Start()
    {
        //FMOD Starter
        Fire_SFX = FMODUnity.RuntimeManager.CreateInstance("event:/Miscellaneous SFX/Fire_Sizzle");
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            Destroy(other.gameObject);

        Fire_SFX.start(); //plays sound

    }
}
