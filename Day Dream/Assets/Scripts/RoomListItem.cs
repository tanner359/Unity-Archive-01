using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text playerCountText;
    [SerializeField] TMP_Text roomStatus;
    public RoomInfo info;

    public void SetUp(RoomInfo _info)
    {      
        info = _info;
        text.text = _info.Name;
        playerCountText.text = _info.PlayerCount.ToString() + "/" + _info.MaxPlayers.ToString();
        if (_info.IsOpen)
        {
            roomStatus.text = "Open";
            roomStatus.color = Color.green;
        }
        else if (!_info.IsOpen)
        {
            roomStatus.text = "Closed";
            roomStatus.color = Color.red;
        }
    }
    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info);
    }
}
