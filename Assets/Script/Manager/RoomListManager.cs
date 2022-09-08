using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomListManager : MonoBehaviourPunCallbacks
{
    public GameObject RoomListRoot;
    public GameObject RoomListPrefab;
	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		for(int i=0;i<RoomListRoot.transform.childCount;i++)
		{
			Destroy(RoomListRoot.transform.GetChild(i).gameObject);
		}
		for (int i = 0; i < roomList.Count; i++)
		{
			if (roomList[i].PlayerCount > 0)
			{
				GameObject newRoom = Instantiate(RoomListPrefab, RoomListRoot.transform);
				newRoom.GetComponentInChildren<RoomButton>().roomName.text = roomList[i].Name;
				newRoom.GetComponentInChildren<RoomButton>().maxPlayerNumText.text = roomList[i].MaxPlayers.ToString();
				newRoom.GetComponentInChildren<RoomButton>().currentPlayerNumText.text = roomList[i].PlayerCount.ToString();
			}
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
