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
	/// "���һ�εĹ�����"
	/// </summary>
	public string lastHitterName;
	/// <summary>
	/// "����Ͷ�����λ�ã�������Զ�̵�λ"
	/// </summary>
	public Vector2 attackPosition;

	public Vector3 transformBeforeDefeatedPosition;
	public int transformBeforeDefeatedForward;
	#endregion

}
