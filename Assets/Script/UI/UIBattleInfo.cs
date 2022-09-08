using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class UIBattleInfo : MonoSingleton<UIBattleInfo>
{
    public Text infoText;
    public PhotonView photonView;

	public void Start()
	{
		photonView = GetComponent<PhotonView>();
	}
	[PunRPC]
    public void setText(string text)
	{
        infoText.text = text;
	}
}
