using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MessageManager : MonoSingleTonPun<MessageManager>
{
    [Header("��ഢ������ʾ����Ϣ����")]
	[SerializeField]
    public int maxMessage;
    public List<MessageDefine> messages=new List<MessageDefine>();

	public void Awake()
	{
		EventManager.PlayerEnter += SendNewPlayerInMsg;
	}

	void SendNewPlayerInMsg(Player newPlayer)
	{
		if (PhotonNetwork.IsMasterClient)
		{
			photonView.RPC("ReceiveNewMessage", RpcTarget.All, (int)MessageType.System, "", newPlayer.NickName + "������Ϸ");
		}
	}

	public void AddLocalMessage(int messageTypeId, string sender, string content)
	{
		ReceiveNewMessage(messageTypeId,sender,content);
	}

	[PunRPC]
    public void ReceiveNewMessage(int messageTypeId,string sender,string content)
	{
		if (messages.Count >= maxMessage)
		{
            for(int i = 1; i <= maxMessage - 1; i++)
			{
                messages[i - 1] = messages[messages.Count-maxMessage+i];
			}
            messages[maxMessage - 1] = new MessageDefine((MessageType)messageTypeId, sender, content);
			if (messages.Count > maxMessage)
			{
				for(int i = messages.Count-1; i >=maxMessage ; i--)
				{
					messages.RemoveAt(i);
				}
			}
		}
		else
		{
			messages.Add(new MessageDefine((MessageType)messageTypeId ,sender, content));
		}
	}
}
