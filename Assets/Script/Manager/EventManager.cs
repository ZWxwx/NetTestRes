using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class EventManager : MonoBehaviourPunCallbacks
{
    public static Action<BulletController,EntityController> EntityBeHit= EntityBeHitHandler;
	public static Action<EntityController, string> EntityDefeated = EntityDefeatedHandler;
	public static void EntityBeHitHandler(BulletController bullet, EntityController entity)
	{
		if (bullet.bulletInfo.leftDamageTime <= 0)
		{
			Destroy(bullet.gameObject);
		}
	}

	public static void EntityDefeatedHandler(EntityController victim,string murderer) {
		if (victim.photonView.IsMine)
		{
			victim.entityInfo.transformBeforeDefeatedForward = (int)victim.transform.localScale.x;
			victim.entityInfo.transformBeforeDefeatedPosition = victim.transform.position;
			GameObject body = PhotonNetwork.Instantiate("DefeatedBody", victim.entityInfo.transformBeforeDefeatedPosition, Quaternion.identity);
			body.GetComponent<PhotonView>().RPC("ReceiveInitialData", RpcTarget.All, DataManager.Instance.Entities[victim.entityInfo.entityDataId].DefeatedAnimId);
			body.transform.localScale = new Vector3(victim.entityInfo.transformBeforeDefeatedForward, 1, 1);
			if (PhotonNetwork.InRoom)
			{
				if (victim.Equals((EntityController)PlayerManager.Instance.currentPlayer))
				{
					GameManager.Instance.ResetRespawnButton(victim.photonView.Owner.NickName);
				}
				PhotonNetwork.Destroy(victim.gameObject);
			}

			
		}
	}


}
