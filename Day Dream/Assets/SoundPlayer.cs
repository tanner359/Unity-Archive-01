using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    [SerializeField] AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        PlaySound();
    }

    public void PlaySound()
    {
        AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length - 1)], Vector3.zero);
    }
}
