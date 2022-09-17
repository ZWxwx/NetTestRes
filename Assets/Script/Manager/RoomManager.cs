using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using System;

public class RoomManager : MonoSingleTonPun<RoomManager>
{
	public void Awake()
	{
		PhotonNetwork.AutomaticallySyncScene = true; 
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			LeaveRoom();
		}
		string playerList = "";
		foreach(var player in PhotonNetwork.PlayerList)
		{
			playerList += player.NickName;
		}
		//Debug.Log(playerList);
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		base.OnPlayerEnteredRoom(newPlayer);
		RaiseEventManager.Instance.SendPlayerEnterEvent(newPlayer);


		//PlayerManager.Instance.photonView.RPC("ReceiveInitialData", RpcTarget.All, newPlayer.NickName);


		//MessageManager.Instance.photonView.RPC("ReceiveNewMessage", RpcTarget.All, (int)MessageType.System, "", newPlayer.NickName + "加入房间");
	}

	public void LeaveRoom()
	{
		
		PhotonNetwork.LeaveRoom();
		
	}
	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		base.OnPlayerLeftRoom(otherPlayer);
		RaiseEventManager.Instance.SendPlayerLeftEvent(otherPlayer);
	}
	public override void OnLeftRoom()
	{
		base.OnLeftRoom();
		SceneManager.LoadScene(0);
	}

}
