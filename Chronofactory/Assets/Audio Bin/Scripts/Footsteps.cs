using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [FMODUnity.EventRef] //Event Caller
    public string soundInput;
    bool playerMovement;
    public float walkingSpeed;

    void Update()
    {
        if (Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01f)
        {
            //Debug.Log("Audio.Log: Player is Moving");
            playerMovement = true;
        }
        else if (Input.GetAxis("Vertical") == 0 || Input.GetAxis("Horizontal") == 0)
        {
            //Debug.Log("Audio.Log: Player is not moving");
            playerMovement = false;
        }
    }

    void Footsteps_Sounds()
    {
        if(playerMovement == true)
        {
            Debug.Log("Audio.Log: Player is Moving");
            FMODUnity.RuntimeManager.PlayOneShot(soundInput);
        }
    }

    void Start()
    {
        InvokeRepeating("Footsteps_Sounds", 0, walkingSpeed);
    }

    void OnDisable()
    {
        playerMovement = false;
    }
    //I will add comment to each line in the future :-P
}
