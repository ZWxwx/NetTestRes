using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameInfo : MonoBehaviour
{
    public Text pingText;
    public Text netStateText;
    void Update()
    {
        pingText.text = PhotonNetwork.GetPing().ToString();
        netStateText.text = PhotonNetwork.NetworkClientState.ToString();
    }
}
