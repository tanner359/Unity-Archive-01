using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tile_Load_Level : MonoBehaviour
{
    public Animator transition_Anim;
    private GameObject deletThisLol;
    int nextSceneIndex;

    private void Start()
    {
        deletThisLol = GameObject.FindGameObjectWithTag("music");
    }

    void Stepped_On()
    {
        if (deletThisLol != null)
        {
            Destroy(deletThisLol);
        }
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        transition_Anim.SetTrigger("end");
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(nextSceneIndex);
    }
}
