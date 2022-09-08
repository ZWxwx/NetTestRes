using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class HealthBar : MonoBehaviourPunCallbacks
{
	public EntityInfo entityInfo;
	public EntityController owner;
	public GameObject barFill;


	public void SetHealthBarColor()
	{
		barFill.GetComponent<Image>().color = (Team)entityInfo.teamId == Team.Red ? Color.red : ((Team)entityInfo.teamId == Team.Blue ? Color.blue : Color.grey);
		
	}
	public void Update()
	{
		
		GetComponent<Slider>().value = (entityInfo.CurrentHealth / entityInfo.maxHealth) < 0 ? 0f: (entityInfo.CurrentHealth / entityInfo.maxHealth) >1?1f: entityInfo.CurrentHealth / entityInfo.maxHealth;
		SetHealthBarColor();
		if (entityInfo.CurrentHealth < 0)
		{
			if (photonView.IsMine)
			{
				owner.onDefeated();
			}
		}
	}
}
