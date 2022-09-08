using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomButton : MonoBehaviour
{
    public Text roomName;
	public Text currentPlayerNumText;
	public Text maxPlayerNumText;
	public Button button;

	private void Start()
	{
		button.onClick.AddListener(EnterRoom);
	}

	private void Update()
	{
	}
	public void EnterRoom()
	{
		RoomOptions options = new RoomOptions();
		PhotonNetwork.JoinOrCreateRoom(roomName.text, options, default);
	}
}
