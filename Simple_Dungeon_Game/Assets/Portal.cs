using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    public int sceneToLoad;
    GameManager gm;
    // Start is called before the first frame update
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gm.LoadLevel(sceneToLoad);
    }
}
