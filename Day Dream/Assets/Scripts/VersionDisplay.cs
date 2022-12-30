using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text versionText;

    private void Start()
    {
        versionText.text = Application.version.ToString();       
    }
}
