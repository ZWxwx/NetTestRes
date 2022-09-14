using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInfo : MonoSingleton<UIPlayerInfo>
{
	public PlayerController pc;
	public Slider healBarFill;
	public Text healthText;
	public Text moneyText;
	public void setPlayer(PlayerController pc)
	{
		this.pc = pc;
	}

	public void Update()
	{
		if (pc != null)
		{
			healBarFill.value = pc.entityInfo.CurrentHealth / pc.entityInfo.maxHealth;
			healthText.text = string.Format("{0}/{1}", pc.entityInfo.CurrentHealth.ToString(), pc.entityInfo.maxHealth.ToString());
			moneyText.text = PlayerManager.Instance.playerMoneys[PhotonNetwork.LocalPlayer.NickName].ToString();
		}
	}
}
