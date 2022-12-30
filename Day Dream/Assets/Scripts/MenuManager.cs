using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public GameObject credits;
    public GameObject patchNotes;

    bool credits_Enabled = false;
    bool patchNotes_Enabled = false;

    [SerializeField] menu[] menus;

    void Awake()
    {
        Instance = this;
    }
    public void OpenMenu(string menuName)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if(menus[i].menuName == menuName)
            {
                menus[i].Open();
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }
    public void OpenMenu(menu menu)
    {
        menu.Open();
    }
    public void CloseMenu(menu menu)
    {
        menu.Close();
    }

    public void Toggle_Credits()
    {
        credits_Enabled = !credits_Enabled;
        credits.SetActive(credits_Enabled);
        if (patchNotes_Enabled)
        {
            TogglePatchNotes();
        }
    }
    public void TogglePatchNotes()
    {
        patchNotes_Enabled = !patchNotes_Enabled;
        patchNotes.SetActive(patchNotes_Enabled);
        if (credits_Enabled)
        {
            Toggle_Credits();
        }
    }
}
