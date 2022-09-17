using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class EntityAnimEvent : MonoBehaviour
{
	public AIEntityController thisAIEntity;
	//public EntityController thisEntity;
	Collider2D[] nearbyCollider;


	#region temp
	EntityController a;
	AIEntityController aec;
	PlayerController pc;

	#endregion


	public Animator anim;

	/*½üÕ½*/
	public void HitTarget()
	{
		nearbyCollider = Physics2D.OverlapCircleAll(transform.position, DataManager.Instance.Entities[thisAIEntity.entityInfo.entityDataId].AttackRange);
		foreach (var collider in nearbyCollider)
		{
			if (collider.TryGetComponent<EntityController>(out a) && a == thisAIEntity.currentTarget)
			{
				//thisAIEntity.currentTarget.entityInfo.CurrentHealth -= DataManager.Instance.Entities[thisAIEntity.entityInfo.entityDataId].Attack;
				thisAIEntity.currentTarget.onHit(DataManager.Instance.Entities[this.thisAIEntity.entityInfo.entityDataId].Name, DataManager.Instance.Entities[thisAIEntity.entityInfo.entityDataId].Attack);
			}
		}
	}

	/*Ô¶³Ì*/
	public void ShotTarget()
	{
		nearbyCollider = Physics2D.OverlapCircleAll(transform.position, DataManager.Instance.Entities[thisAIEntity.entityInfo.entityDataId].AttackRange);
		foreach (var collider in nearbyCollider)
		{
			if (collider.TryGetComponent<EntityController>(out a) && a == thisAIEntity.currentTarget)
			{
				if(collider.TryGetComponent(out aec))
				{
					ShotAEntity(aec);
				}
				else if(collider.TryGetComponent(out pc))
				{
					ShotAEntity(pc);
				}
			}
		}
	}

	public void ShotAEntity(EntityController entity)
	{
		if (this.thisAIEntity.photonView.IsMine)
		{
			GameObject bullet = PhotonNetwork.Instantiate("IceCone", (Vector2)transform.position + GameTool.SerPosition(DataManager.Instance.Entities[thisAIEntity.entityInfo.entityDataId].AttackPosition), Quaternion.identity);
			bullet.GetComponent<PhotonView>().RPC("ReceiveInitialData", RpcTarget.All, DataManager.Instance.Entities[thisAIEntity.entityInfo.entityDataId].BulletDataId, DataManager.Instance.Entities[thisAIEntity.entityInfo.entityDataId].Name);
			bullet.GetComponent<BulletController>().Angle = Mathf.Atan2(entity.transform.position.y +
				 -transform.position.y - GameTool.SerPosition(DataManager.Instance.Entities[this.thisAIEntity.entityInfo.entityDataId].AttackPosition).y
				 , entity.transform.position.x - transform.position.x - GameTool.SerPosition(DataManager.Instance.Entities[this.thisAIEntity.entityInfo.entityDataId].AttackPosition).x) * 180f / Mathf.PI;
			bullet.GetComponent<BulletController>().bulletInfo.teamId = this.thisAIEntity.entityInfo.teamId;
			
		}
	}

}
