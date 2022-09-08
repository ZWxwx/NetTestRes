using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Lancher : MonoBehaviourPunCallbacks
{
	// Start is called before the first frame update
	void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

	// Update is called once per frame
	public override void OnConnectedToMaster()
	{
		base.OnConnectedToMaster();
        Debug.Log("WelCome Player");

        PhotonNetwork.JoinOrCreateRoom("Room", new Photon.Realtime.RoomOptions() { MaxPlayers = 4 }, default);
	}

	public override void OnJoinedRoom()
	{
		PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
	}
}
