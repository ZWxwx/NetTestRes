using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class HealthBar : MonoBehaviourPunCallbacks
{
	public Vector3 originSize;
	public EntityInfo entityInfo;
	public EntityController owner;
	public GameObject barFill;


	public void SetHealthBarColor()
	{
		barFill.GetComponent<Image>().color = (Team)entityInfo.teamId == Team.Red ? Color.red : ((Team)entityInfo.teamId == Team.Blue ? Color.blue : Color.grey);
		
	}

	public void Start()
	{
		originSize = transform.localScale;
	}

	public void Update()
	{
		transform.localScale = new Vector3(originSize.x * Mathf.Log((1 + entityInfo.maxHealth / 250f)), originSize.y * Mathf.Log((1 + entityInfo.maxHealth / 250f)), originSize.z);
		GetComponent<Slider>().value = (entityInfo.CurrentHealth / entityInfo.maxHealth) < 0 ? 0f: (entityInfo.CurrentHealth / entityInfo.maxHealth) >1?1f: entityInfo.CurrentHealth / entityInfo.maxHealth;
		SetHealthBarColor();
		if (entityInfo.CurrentHealth < 0)
		{
			EventManager.EntityDefeated(owner, owner.lastHitterName);
		}
	}
}
