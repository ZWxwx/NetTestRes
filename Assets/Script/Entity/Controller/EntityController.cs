using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public abstract class EntityController : MonoBehaviourPunCallbacks, IPunObservable
{
	#region Public Fields

	public EntityController currentTarget;

	public EntityInfo entityInfo=new EntityInfo() { isTargetNearby=false};
	[Header("当前实体状态")]
	public AIEntityStatus currentStatus;

	[Header("必要Unity组件")]
	public Animator animator;
	public Rigidbody2D rb;

	[Header("必要其他组件")]
	public UIEntityInfo uIEntityInfo;
	//public HealthBar healthBar;
	public PhotonView pv;

	public GameObject bodyPrefab;

	public Action<string,float> onHit;

	[Header("仅用于已存在场上的物体而非通过加载的物体")]
	public int localEntityId=-1;
	public Team localEntityTeam=Team.None;
	#endregion
	GameObject tempObj;
	#region Protected Fields

	protected Collider2D[] nearbyCollider;
	protected bool isOnHandleDefeated=false;

	#endregion

	#region Public Methods
	public virtual void Awake()
	{
		if (localEntityId < 0)
		{
			enabled = false;
		}
		else
		{
			enabled = true;
			SetInitialData(localEntityId, (int)localEntityTeam, DataManager.Instance.Entities[localEntityId].MaxHealth);
		}
		
	}
	public virtual void Start()
	{
		onHit += this.handleOnHit;
		EventManager.EntityDefeated += OnDefeated;
	}

	[PunRPC]
	public void ReceiveInitialDataByClient(int entityDataId,int teamId,float CurrentHealth, int viewId, string receiver)
	{
		if(PhotonNetwork.NickName!=receiver||photonView.ViewID!=viewId)
		{
			return;
		}
		SetInitialData(entityDataId, teamId, CurrentHealth);
	}

	[PunRPC]
	public void ReceiveInitialData(int entityDataId, int teamId, float CurrentHealth)
	{
		SetInitialData(entityDataId, teamId, CurrentHealth);
	}

	public void SetInitialData(int entityDataId, int teamId, float CurrentHealth) {
		this.entityInfo.entityDataId = entityDataId;
		this.entityInfo.teamId = teamId;
		this.entityInfo.CurrentHealth = CurrentHealth;
		animator.runtimeAnimatorController = DataManager.Instance.Animator[DataManager.Instance.Entities[entityInfo.entityDataId].DefeatedAnimId];
		entityInfo.maxHealth = DataManager.Instance.Entities[entityInfo.entityDataId].MaxHealth;
		this.enabled = true;
		uIEntityInfo.refreshInfo();
	}

	public void SendInitialData(int entityDataId, int teamId, float CurrentHealth)
	{
		photonView.RPC("ReceiveInitialData", RpcTarget.All, entityDataId,teamId,CurrentHealth);
	}


	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		photonView.RPC("ReceiveInitialDataByClient", RpcTarget.All, entityInfo.entityDataId, entityInfo.teamId, entityInfo.CurrentHealth, photonView.ViewID, newPlayer.NickName);
	}

	public virtual void Update()
	{
		if (animator != null)
		{
			animator.SetTrigger(currentStatus.ToString());
		}
	
		transform.position = new Vector3(transform.position.x, transform.position.y, 100+ transform.position.y);

		if (entityInfo.CurrentHealth < 0&& !isOnHandleDefeated&&photonView.IsMine)
		{
			isOnHandleDefeated = true;
			RaiseEventManager.Instance.SendEntityDefeatedEvent(
	entityInfo.isAI ? DataManager.Instance.Entities[entityInfo.entityDataId].Name : photonView.Owner.NickName
	, photonView.ViewID, entityInfo.entityDataId, entityInfo.teamId, entityInfo.isAI,
	transform.position, entityInfo.lastHitterName);

			//EventManager.EntityDefeated(entityInfo, owner.entityInfo.lastHitterName);
		}
	}
	//public IEnumerator onSendDefeatedEvent()
	//{
	//	while (true)
	//	{
			
			
	//		yield return new WaitForSeconds(0.5f);
	//	}
	//}

	#endregion

	#region Other

	public void handleOnHit(string hitterName,float damage)
	{
		entityInfo.lastHitterName = hitterName;
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

	public void OnDefeated(string victimName,int viewID, int entityID, int teamID, bool isVictimAI, Vector2 position, string murderer)
	{
		if (viewID == photonView.ViewID&&((photonView.IsRoomView & PhotonNetwork.IsMasterClient) || photonView.IsMine))
		{

			tempObj = PhotonNetwork.Instantiate("DefeatedBody", transform.position,Quaternion.identity);
			tempObj.GetComponent<DefeatedBody>().photonView.RPC("ReceiveInitialData", RpcTarget.All, DataManager.Instance.Entities[entityInfo.entityDataId].DefeatedAnimId,transform.localScale.x);

			if (this.Equals(GameManager.Instance.redTower))
			{
				LevelManager.Instance.beginEndBattle(Team.Blue);
			}
			else if (this.Equals(GameManager.Instance.blueTower))
			{
				LevelManager.Instance.beginEndBattle(Team.Red);
			}
			PhotonNetwork.Destroy(this.gameObject);
		}
	}

	public override void OnDisable()
	{
		base.OnDisable();
		EventManager.EntityDefeated -= this.OnDefeated;
	}
	#endregion
}
