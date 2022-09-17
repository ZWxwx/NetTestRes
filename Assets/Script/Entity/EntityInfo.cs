using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public struct EntityInfo
{
	#region Public Fields
	public bool isTargetNearby;
	public bool isAI;
	public float iAttackDuring;
	public int entityDataId;
	//public EntityController entity;
	public float maxHealth;
	public float CurrentHealth;
	public int teamId;
	/// <summary>
	/// "最近一次的攻击者"
	/// </summary>
	public string lastHitterName;
	/// <summary>
	/// "发射投掷物的位置，常用于远程单位"
	/// </summary>
	public Vector2 attackPosition;

	public Vector3 transformBeforeDefeatedPosition;
	public int transformBeforeDefeatedForward;
	#endregion

}
