using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public abstract class EntityController : MonoBehaviourPunCallbacks,IPunObservable
{
	#region Public Fields

	public bool isAI;
	public float iAttackDuring;

	public EntityController currentTarget;

	public Collider2D[] nearbyCollider;
	public bool isTargetNearby = false;

	public EntityInfo entityInfo;
	public EntityDefine entityData;

	public AIEntityStatus currentStatus;

	public Animator animator;
	public Rigidbody2D rb;
	public UIEntityInfo uIEntityInfo;
	public HealthBar healthBar;
	public PhotonView pv;

	public GameObject bodyPrefab;

	public Action<string,float> onHit;
	[Header("最近一次的攻击者")]
	public string lastHitterName;
	#endregion

	#region Public Fields

	public virtual void Start()
	{
		onHit += this.handleOnHit;
		RefreshEntity();
	}

	[PunRPC]
	public void ReceiveInitialDataByClient(int entityDataId,int teamId,float CurrentHealth, int viewId, string receiver)
	{
		if(PhotonNetwork.NickName!=receiver||photonView.ViewID!=viewId)
		{
			return;
		}
		this.entityInfo.entityDataId = entityDataId;
		this.entityInfo.teamId = teamId;
		this.entityInfo.CurrentHealth = CurrentHealth;
	}

	[PunRPC]
	public void ReceiveInitialData(int entityDataId, int teamId, float CurrentHealth)
	{
		this.entityInfo.entityDataId = entityDataId;
		this.entityInfo.teamId = teamId;
		this.entityInfo.CurrentHealth = CurrentHealth;
	}

	public void SendInitialData(int viewId,string receiver)
	{
		photonView.RPC("ReceiveInitialDataByClient", RpcTarget.All ,entityInfo.entityDataId,entityInfo.teamId,entityInfo.CurrentHealth, viewId, receiver);
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		SendInitialData(photonView.ViewID,newPlayer.NickName);
	}

	public virtual void Update()
	{
		if (animator != null)
		{
			animator.SetTrigger(currentStatus.ToString());
		}
		transform.position = new Vector3(transform.position.x, transform.position.y, 100+ transform.position.y);
	}
	#endregion

	#region Other

	public void handleOnHit(string hitterName,float damage)
	{
		this.lastHitterName = hitterName;
		this.entityInfo.CurrentHealth-=damage;
	}
	#region abandoned
	//public void DestroyNextFrame()
	//{
	//	StartCoroutine(DestroyThis());
	//}

	//public IEnumerator DestroyThis()
	//{
	//	yield return null;
	//	PhotonNetwork.Destroy(this.gameObject);
	//}

	//private void OnDestroy()
	//{
	//	if (!PhotonNetwork.InRoom)
	//	{
	//		return;
	//	}
	//	if (!isAI)
	//	{
	//		GameManager.Instance.ResetRespawnButton(photonView.Owner.NickName);
	//	}

	//	GameObject body = PhotonNetwork.Instantiate("DefeatedBody", entityInfo.transformBeforeDefeatedPosition, Quaternion.identity);
	//	body.GetComponent<PhotonView>().RPC("ReceiveInitialData", RpcTarget.All, DataManager.Instance.Entities[entityInfo.entityDataId].DefeatedAnimId);
	//	body.transform.localScale= new Vector3(entityInfo.transformBeforeDefeatedForward,1,1);
	//}
	#endregion
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!photonView.IsMine)
		{
			return;
		}
		BulletController bullet;
		if(collision.TryGetComponent<BulletController>(out bullet))
		{
			if (bullet.bulletInfo.teamId != entityInfo.teamId&& bullet.bulletInfo.leftDamageTime>0)
			{
				onHit(bullet.bulletInfo.ownerName, DataManager.Instance.Bullets[bullet.bulletInfo.bulletDataId].Damage);
				bullet.bulletInfo.leftDamageTime -= 1;
				EventManager.EntityBeHit(bullet, this);
			}
		}
	}

	public void RefreshEntity()
	{
		animator.runtimeAnimatorController = DataManager.Instance.Animator[DataManager.Instance.Entities[entityInfo.entityDataId].DefeatedAnimId];
		entityInfo.maxHealth = DataManager.Instance.Entities[entityInfo.entityDataId].MaxHealth;
		entityInfo.CurrentHealth = entityInfo.maxHealth;
		uIEntityInfo.refreshInfo();
	}



	#endregion

	#region IPunObservableImplement
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(entityInfo.CurrentHealth);
		}
		else
		{
			entityInfo.CurrentHealth = (float)stream.ReceiveNext();
		}
	}
	#endregion
}
