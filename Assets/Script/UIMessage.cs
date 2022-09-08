using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class UIMessage : MonoBehaviour
{
	public Text messageField;
	public InputField messageInputField;
	public void SendTalkMessage()
	{
		MessageManager.Instance.GetComponent<PhotonView>().RPC("ReceiveNewMessage", RpcTarget.All, (int)MessageType.Talk,PhotonNetwork.NickName ,messageInputField.text);
		messageInputField.text = "";
	}


	private void Update()
	{
		messageField.text = "";
		if (MessageManager.Instance.messages.Count>0)
		{
			foreach (var msg in MessageManager.Instance.messages)
			{
				messageField.text += string.Format("[{0}]{1}: {2}\n", msg.Type.ToString(), msg.Sender,msg.Content);
			}
		}
	}
}
