using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class EntityInfo : MonoBehaviour
{
	#region Public Fields
	public int entityDataId;
	public EntityController entity;
	public float maxHealth;
	public float CurrentHealth;
	public int teamId;
	[Header("发射投掷物的位置，常用于远程单位")]
	public Vector2 attackPosition;

	public Vector3 transformBeforeDefeatedPosition;
	public int transformBeforeDefeatedForward;
	#endregion

}
