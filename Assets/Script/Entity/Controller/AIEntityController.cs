using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AIEntityController : EntityController
{
	[Header("实际射击范围和数据射击范围的误差")]
	public static float fixedAttackRange=0.5f;
	static float resetNearbyTargetDuring = 0.8f;
	public bool isTower=false;


	public override void Awake()
	{
		base.Awake();
		entityInfo.isAI = true;
	}
	

	public override void Start()
	{
		base.Start();
		rb = GetComponent<Rigidbody2D>();
		currentStatus = AIEntityStatus.Idle;
		StartCoroutine(constantlyResetNearbyTarget());
		StartCoroutine(constantlyResetTarget());
		StartCoroutine(MoveToTarget());
	}

	public void ResetAttackTarget()
	{
		float minDistance=2^32-1;
		bool flag=false;

		#region 查找最近敌方单位，若视野内无敌方单位则目标置空

		
		nearbyCollider = Physics2D.OverlapCircleAll(transform.position, DataManager.Instance.Entities[entityInfo.entityDataId]!=null? DataManager.Instance.Entities[entityInfo.entityDataId].ViewRange:0f);
		AIEntityController aec;
		PlayerController pc;

		foreach(var col in nearbyCollider)
		{
			if (col.TryGetComponent(out aec))
			{
				if (aec.entityInfo.teamId != entityInfo.teamId)
				{
					flag = true;
					if ((aec.transform.position - transform.position).magnitude < minDistance)
					{

						currentTarget = aec;
						minDistance = (aec.transform.position - transform.position).magnitude;
					}
				}
			}
			else if (col.TryGetComponent(out pc))
			{
				if (pc.entityInfo.teamId != entityInfo.teamId)
				{
					flag = true;
					if ((pc.transform.position - transform.position).magnitude < minDistance)
					{

						currentTarget = pc;
						minDistance = (pc.transform.position - transform.position).magnitude;
					}
				}
			}
		}
		if (!flag)
		{
			currentTarget = null;
		}

		#endregion
	}

	IEnumerator constantlyResetTarget()
	{
		while (true)
		{
			ResetAttackTarget();
			yield return new WaitForSeconds(1f);
		}
	}

	IEnumerator MoveToTarget()
	{
		while (true)
		{
			if (currentStatus == AIEntityStatus.Move) {
				if (currentTarget != null )
				{

					rb.velocity = (currentTarget.transform.position - transform.position).normalized*DataManager.Instance.Entities[entityInfo.entityDataId].Speed;
				
				}
				else
				{
					rb.velocity = Vector3.zero;
				}
				
			}
			else
			{
				rb.velocity = Vector3.zero;
			}
			yield return null;
		}
	}

	/*不同于AttackTarget,该Target为位于攻击范围的可攻击实体而非追随的攻击目标*/
	public IEnumerator constantlyResetNearbyTarget()
	{
		while (true)
		{
			ResetNearbyTarget();
			yield return new WaitForSeconds(resetNearbyTargetDuring);
		}
	}
	public void ResetNearbyTarget()
	{
		nearbyCollider = Physics2D.OverlapCircleAll(transform.position, DataManager.Instance.Entities[entityInfo.entityDataId].AttackRange);
		entityInfo.isTargetNearby = false;
		foreach (var collider in nearbyCollider)
		{
			EntityController a;
			if (collider.TryGetComponent<EntityController>(out a) && a == currentTarget)
			{
				entityInfo.isTargetNearby = true;
			}
		}


	}


	public override void Update()
	{
		base.Update();
		if (currentTarget != null) {
			
			if (entityInfo.isTargetNearby) {
				if (entityInfo.iAttackDuring ==0) {
					currentStatus = AIEntityStatus.Attack1;
					entityInfo.iAttackDuring = DataManager.Instance.Entities[entityInfo.entityDataId].AttackDuring;
				}
				else
				{
					currentStatus = AIEntityStatus.Idle;
				}

			}
			else
			{
				currentStatus = AIEntityStatus.Move;
			}
			/*朝向目标*/
			if ((currentTarget.transform.position.x - transform.position.x) * transform.localScale.x < 0) {
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
		}
		else
		{
			if (entityInfo.teamId == (int)Team.Red&&GameManager.Instance.blueTower!=null)
			{
				currentTarget = GameManager.Instance.blueTower;
			}
			else if(entityInfo.teamId == (int)Team.Blue && GameManager.Instance.redTower != null)
			{
				currentTarget = GameManager.Instance.redTower;
			}
			else
			{
				currentStatus = AIEntityStatus.Idle;
			}
		}

		if (entityInfo.iAttackDuring - Time.deltaTime > 0)
		{
			entityInfo.iAttackDuring -= Time.deltaTime;
		}
		else
		{
			entityInfo.iAttackDuring = 0;
		}

	}


}
