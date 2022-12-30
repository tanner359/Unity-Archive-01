using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    int current_Menu_Op = 0;
    int current_Music = 0;

    public GameObject menu_cursor;
    public Transform start_pos;
    public Transform quit_pos;
    public Transform music_pos;

    public GameObject play_text;
    public GameObject quit_text;
    public GameObject factory_song;
    public GameObject medieval_song;

    public Material def_shader;
    public Material font_shader;

    // Start is called before the first frame update
    void Start()
    {
        menu_cursor.transform.position = start_pos.position;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            current_Menu_Op -= 1;

            if (current_Menu_Op < 0)
                current_Menu_Op = 2;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            current_Menu_Op += 1;

            if (current_Menu_Op > 2)
                current_Menu_Op = 0;
        }

        if (current_Menu_Op == 0)
        {
            menu_cursor.transform.position = Vector3.Slerp(menu_cursor.transform.position, start_pos.position, 0.2f);
            play_text.transform.Rotate(new Vector3(0f, 0f, 1f));
        }
        else if (current_Menu_Op == 1)
        {
            menu_cursor.transform.position = Vector3.Slerp(menu_cursor.transform.position, quit_pos.position, 0.2f);
            quit_text.transform.Rotate(new Vector3(0f, 0f, 1f));
        }
        else if (current_Menu_Op == 2)
        {
            menu_cursor.transform.position = Vector3.Slerp(menu_cursor.transform.position, music_pos.position, 0.2f);
        }

        if (current_Music == 0) // Factory Theme
        {
            factory_song.GetComponent<TextMeshProUGUI>().fontMaterial = font_shader;
            medieval_song.GetComponent<TextMeshProUGUI>().fontMaterial = def_shader;
            Music_Player.musicToggle = 0;
        }
        else if (current_Music == 1) // Medieval Theme
        {
            medieval_song.GetComponent<TextMeshProUGUI>().fontMaterial = font_shader;
            factory_song.GetComponent<TextMeshProUGUI>().fontMaterial = def_shader;
            Music_Player.musicToggle = 1;
        }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (current_Menu_Op == 0)
                SceneManager.LoadScene("Tutorial_Level");
            else if (current_Menu_Op == 1)
                Application.Quit();
            else if (current_Menu_Op == 2)
            {
                current_Music += 1;

                if (current_Music > 1)
                    current_Music = 0;
            }
        }
    }
}
