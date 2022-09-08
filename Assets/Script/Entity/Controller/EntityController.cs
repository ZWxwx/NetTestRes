using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public abstract class EntityController : MonoBehaviourPunCallbacks
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
	public Action onDefeated;
	[Header("最近一次的攻击者")]
	public string lastHitterName;
	#endregion

	#region Public Fields

	public virtual void Start()
	{
		onHit += this.handleOnHit;
		onDefeated += this.handleOnDefeated;
		RefreshEntity();
	}

	[PunRPC]
	public void ReceiveInitialData(int entityDataId)
	{
		this.entityInfo.entityDataId = entityDataId;
	}

	public virtual void Update()
	{
		if (animator != null)
		{
			animator.SetTrigger(currentStatus.ToString());
		}
		WhileUpdate();
	}

	public abstract void WhileUpdate();
	#endregion

	#region Other

	public void handleOnHit(string hitterName,float damage)
	{
		this.lastHitterName = hitterName;
		this.entityInfo.CurrentHealth-=damage;
	}

	public void handleOnDefeated()
	{
		EventManager.EntityDefeated(this,lastHitterName);
	}

	public void DestroyNextFrame()
	{
		StartCoroutine(DestroyThis());
	}

	public IEnumerator DestroyThis()
	{
		yield return null;
		PhotonNetwork.Destroy(this.gameObject);
	}

	private void OnDestroy()
	{
		if (!isAI)
		{
			GameManager.Instance.ResetRespawnButton(photonView.Owner.NickName);
			PlayerManager.Instance.players.Remove(photonView.Owner.NickName);
		}
		GameObject body = PhotonNetwork.Instantiate("DefeatedBody", entityInfo.transformBeforeDefeatedPosition, Quaternion.identity);
		body.GetComponent<PhotonView>().RPC("ReceiveInitialData", RpcTarget.All, DataManager.Instance.Entities[entityInfo.entityDataId].DefeatedAnimId);
		body.transform.localScale= new Vector3(entityInfo.transformBeforeDefeatedForward,1,1);
	}

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

	#endregion
}
