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
	[Header("����Ͷ�����λ�ã�������Զ�̵�λ")]
	public Vector2 attackPosition;

	public Vector3 transformBeforeDefeatedPosition;
	public int transformBeforeDefeatedForward;
	#endregion

}
