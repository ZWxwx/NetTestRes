using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class RoomManager : MonoBehaviourPunCallbacks
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			LeaveRoom();
		}
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
		
	}

	public override void OnLeftRoom()
	{
		base.OnLeftRoom();
		SceneManager.LoadScene(0);
	}
}
