using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class MessageManager : MonoSingleton<MessageManager>
{
    [Header("最多储存与显示的信息条数")]
	[SerializeField]
    public int maxMessage;
    public List<MessageDefine> messages=new List<MessageDefine>();

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
