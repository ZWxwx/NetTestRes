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
    public Text connectingTip;
    string gameVersion = "1";

    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        
    }

    public IEnumerator OnConnect()
	{
        int i = 0;
        while (true) {
            i++;
            if (connectingTip == null)
            {
                continue;
            }
            yield return new WaitForSecondsRealtime(0.5f);
			switch (i/3)
			{
                case 1:
                    connectingTip.text = "连接中.";
                    break;
                case 2:
                    connectingTip.text = "连接中..";
                    break;
                case 0:
                    connectingTip.text = "连接中...";
                    break;
                default:
					break;
			}
			
        }
    }

    public IEnumerator OnConnectToRoom()
    {
        int i = 0;
        while (true)
        {
            i++;
            if (connectingTip == null)
            {
                continue;
            }
            yield return new WaitForSecondsRealtime(0.5f);
            switch (i / 3)
            {
                case 1:
                    connectingTip.text = "进入房间中.";
                    break;
                case 2:
                    connectingTip.text = "进入房间中..";
                    break;
                case 0:
                    connectingTip.text = "进入房间中...";
                    break;
                default:
                    break;
            }
        }

    }

    public void SetConnectingTipNull(List<IEnumerator> iens)
	{
        foreach (var ien in iens)
        {
            StopCoroutine(ien);
        }
        connectingTip.text = "";
	}

    public IEnumerator OnReConnect()
    {
        int i = 0;
        while (true)
        {
            i++;
            if (connectingTip == null)
            {
                continue;
            }
            yield return new WaitForSecondsRealtime(0.5f);
            switch (i / 3)
            {
                case 1:
                    connectingTip.text = "重连中.";
                    break;
                case 2:
                    connectingTip.text = "重连中..";
                    break;
                case 0:
                    connectingTip.text = "重连中...";
                    break;
                default:
                    break;
            }

        }
    }
    void Start()
    {
        StartCoroutine(OnConnect());
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
        SetConnectingTipNull(new List<IEnumerator>() { OnConnect(), OnReConnect(),OnConnectToRoom() });

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
        //if (roomInput.text.Length < 2)
        //{
        //    roomTip.SetActive(true);
        //    return;
        //}
        roomUI.SetActive(false);
        RoomOptions options = new RoomOptions();
        StartCoroutine(OnConnectToRoom());
        PhotonNetwork.JoinOrCreateRoom(roomInput.text, options, default);

	}

	public override void OnJoinedRoom()
	{
        SetConnectingTipNull(new List<IEnumerator>() { OnConnect(), OnReConnect(), OnConnectToRoom() });
        PhotonNetwork.LoadLevel(1);
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public override void OnDisconnected(DisconnectCause cause)
	{
		base.OnDisconnected(cause);
        PhotonNetwork.ConnectUsingSettings();
        Debug.LogWarning("重连中");
        StartCoroutine(OnReConnect());
    }
}
