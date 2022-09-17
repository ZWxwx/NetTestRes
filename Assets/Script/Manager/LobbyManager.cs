using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoSingleTonPun<LobbyManager>
{
	[SerializeField]
    public string[] onlinePlayerNames=new string[20];
	public Text nameListText;

	public void Update()
	{
		//nameListText.text = "";
		//if (nameListText != null)
		//{
		//	foreach(string name in onlinePlayerNames)
		//	{
		//		nameListText.text += "\n"+name ;
		//	}
			

		//}
	}

}
