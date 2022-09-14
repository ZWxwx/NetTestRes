using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class PlayerController : EntityController
{
    
    #region Public Fields

    public float speed;
    public Text nameUI;
    public GameObject bullet;
    public PlayerBattleInfo playerBattleInfo=new PlayerBattleInfo();

	#endregion

	#region IPunObservable Implements


	#endregion

	#region Public Methods
	#endregion

	public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        nameUI.text = photonView.Owner.NickName;
#if UNITY_ANDROID
        AndriodInputManager.Instance.attackButton.onClick.AddListener(shotOneBulletToNearestTarget);
#endif
    }
    public IEnumerator shotOneBullet(Transform target,float time)
	{
        iAttackDuring = DataManager.Instance.Entities[entityInfo.entityDataId].AttackDuring;
        currentStatus = AIEntityStatus.Attack1;
        yield return new WaitForSeconds(time);
		if (target == null)
		{
            yield break;
		}
		Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		bullet = PhotonNetwork.Instantiate("IceCone", transform.position + new Vector3(0, 0.5f), Quaternion.identity);
        bullet.GetComponent<PhotonView>().RPC("ReceiveInitialData", RpcTarget.All, DataManager.Instance.Entities[entityInfo.entityDataId].BulletDataId, photonView.Owner.NickName);
        bullet.GetComponent<BulletController>().bulletInfo.bulletDataId = 101;
        bullet.GetComponent<BulletController>().Angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * 180f / Mathf.PI;		   
		bullet.GetComponent<BulletController>().bulletInfo.teamId = entityInfo.teamId;
        

		Debug.LogFormat("{0} {1}", Mathf.Atan2(mPosition.y - transform.position.y, mPosition.x - transform.position.x), Mathf.Atan2(mPosition.y - transform.position.y, mPosition.x - transform.position.x) * 180f / Mathf.PI);
		currentStatus = AIEntityStatus.Idle;
        
    }

/// <summary>
/// 暂时用于android
/// </summary>
    public void shotOneBulletToNearestTarget()
    {
		if (iAttackDuring > 0)
		{
            return;
		}
        nearbyCollider = Physics2D.OverlapCircleAll(transform.position,DataManager.Instance.Entities[entityInfo.entityDataId].AttackRange);
        float minDistance=65535f;
        EntityController minDistanceEC=null;
        foreach(var col in nearbyCollider)
		{
            if (col.tag == "Entity")
            {
                if (col.GetComponent<AIEntityController>() == null)
                {
                    currentTarget = col.GetComponent<PlayerController>();
                }
                else
                {
                    currentTarget = col.GetComponent<AIEntityController>();
                }
				if (currentTarget.Equals(this)||currentTarget.entityInfo.teamId==entityInfo.teamId)
				{
                    continue;
				}
                if((currentTarget.transform.position - transform.position).magnitude < minDistance)
				{
                    minDistance = (currentTarget.transform.position - transform.position).magnitude;
                    minDistanceEC = currentTarget;
				}
            }
        }
		if (minDistanceEC != null)
		{
            StartCoroutine(shotOneBullet(minDistanceEC.transform, 0.5f));
		}
    }


    public void Update()
    {
        base.Update();
        #region Moving Control

        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }

#if UNITY_STANDALONE_WIN
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
#endif
#if UNITY_ANDROID
        //安卓端移动方式
        float horizontal = AndriodInputManager.Instance.moveStick.Horizontal;
        float vertical = AndriodInputManager.Instance.moveStick.Vertical;
#endif 
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Cirno_Move"))
        {
            rb.velocity = new Vector2(horizontal, vertical) * speed;
        }
		else
		{
            rb.velocity = new Vector2(0f, 0f);
        }
        #region 转向
		if (transform.localScale.x < 0 && horizontal > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); 
        }

        else if (transform.localScale.x > 0 && horizontal < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
		#endregion
        #region 由是否移动决定状态
		if (horizontal != 0 || vertical != 0)
        {
            currentStatus = AIEntityStatus.Move;
        }

        else
        {
            currentStatus = AIEntityStatus.Idle;

        }

		#endregion

		#region Target Control

#if UNITY_UNITY_STANDALONE_WIN
		if (Input.GetButtonDown("Fire1")&&iAttackDuring==0)
		{
            Collider2D targetCol=null;
			nearbyCollider = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			foreach (var item in nearbyCollider)
			{
				if (item.tag=="Entity")
				{
					if (item.GetComponent<AIEntityController>() == null)
					{
                        currentTarget = item.GetComponent<PlayerController>();
                    }
					else
					{
                        currentTarget = item.GetComponent<AIEntityController>();
                    }
                    targetCol = item;
                    break;
				}
			}

			if (targetCol != null)
			{
                Debug.Log(targetCol.gameObject.name);
                if ((targetCol.transform.position - transform.position).magnitude < DataManager.Instance.Entities[entityInfo.entityDataId].AttackRange)
				{
					StartCoroutine(shotOneBullet(targetCol.transform,0.5f));
                }
            }
		}
#endif

#endregion

		#region 运转供给冷却
		if (iAttackDuring - Time.deltaTime > 0)
        {
            iAttackDuring -= Time.deltaTime;
        }
        else
        {
            iAttackDuring = 0;
        }
#endregion

#region Other



#endregion
    }

    public void Respawn()
	{
        
    }

#endregion

}
