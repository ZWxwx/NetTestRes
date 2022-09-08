using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkLauncher : MonoBehaviourPunCallbacks
{
    public GameObject nameUI;
    public GameObject roomUI;
    public InputField nameInput;
    public InputField roomInput;
    public GameObject roomTip;

    string gameVersion = "1";

    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

	public override void OnConnectedToMaster()
	{
		if (PhotonNetwork.NickName != "")
		{
            roomUI.SetActive(true);
            PhotonNetwork.JoinLobby();
            Debug.Log("Welcome" + PhotonNetwork.NickName);
            return;
		}
        nameUI.SetActive(true);
		
	}

    public void confirmNameButton()
	{
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.NickName = nameInput.text;
        nameUI.SetActive(false);
        roomUI.SetActive(true);
        PhotonNetwork.JoinLobby();
        Debug.Log("Welcome" + PhotonNetwork.NickName);
	}

    public void joinOrPlayButton()
	{
        if (roomInput.text.Length < 2)
        {
            roomTip.SetActive(true);
            return;
        }
        roomUI.SetActive(false);
        RoomOptions options = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom(roomInput.text, options, default);
	}

	public override void OnJoinedRoom()
	{
        PhotonNetwork.LoadLevel(1);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
