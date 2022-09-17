using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class HealthBar : MonoBehaviour
{
	public bool onRefresh=true;
	public Vector3 originSize;
	public EntityController entity;
	public GameObject barFill;
	public void SetHealthBarColor()
	{
		barFill.GetComponent<Image>().color = (Team)entity.entityInfo.teamId == Team.Red ? Color.red : ((Team)entity.entityInfo.teamId == Team.Blue ? Color.blue : Color.grey);
	}

	public void Start()
	{
		originSize = transform.localScale;
	}

	public void RefreshHealth()
	{
		transform.localScale = new Vector3(originSize.x * Mathf.Log((1 + entity.entityInfo.maxHealth / 250f)), originSize.y * Mathf.Log((1 + entity.entityInfo.maxHealth / 250f)), originSize.z);
		GetComponent<Slider>().value = (entity.entityInfo.CurrentHealth / entity.entityInfo.maxHealth) < 0 ? 0f : (entity.entityInfo.CurrentHealth / entity.entityInfo.maxHealth) > 1 ? 1f : entity.entityInfo.CurrentHealth / entity.entityInfo.maxHealth;
	}
	public void Update()
	{
		if (onRefresh&&entity.enabled)
		{
			RefreshHealth();
			SetHealthBarColor();
		}
	}
}
