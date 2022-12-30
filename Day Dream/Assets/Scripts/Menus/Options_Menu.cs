using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options_Menu : MonoBehaviour
{
    public Resolution current_Res;
    bool fullscreen;

    public void Set_Resolution(Resolution res)
    {
        Screen.SetResolution((int)res.resolution.x, (int)res.resolution.y, fullscreen);
        current_Res = res;
    }

    public void Set_Fullscreen()
    {
        fullscreen = true;
        Set_Resolution(current_Res);
    }

    public void Set_Windowed()
    {
        fullscreen = false;
        Set_Resolution(current_Res);
    }
}
