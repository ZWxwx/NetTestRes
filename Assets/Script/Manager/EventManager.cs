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
		
	}

	public static void EntityDefeatedHandler(EntityController victim,string murderer) {
		if (victim.photonView.IsMine)
		{
			victim.entityInfo.transformBeforeDefeatedForward = (int)victim.transform.localScale.x;
			victim.entityInfo.transformBeforeDefeatedPosition = victim.transform.position;
			victim.DestroyNextFrame();
		}
	}


}
