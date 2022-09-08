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
    public int money=100;
    

    #endregion

    #region IPunObservable Implements


    #endregion

    #region Public Methods

    public override void Start()
    {
        base.Start();
        GameManager.Instance.onPlayerJoined(this);
        rb = GetComponent<Rigidbody2D>();
        //joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        if (photonView.IsMine)
            nameUI.text = PhotonNetwork.NickName;
        else
            nameUI.text = photonView.Owner.NickName;
    }
    public IEnumerator shotOneBullet(Transform target,float time)
	{
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


	public override void WhileUpdate()
    {
        #region Moving Control

        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Cirno_Move"))
        {
            rb.velocity = new Vector2(horizontal, vertical) * speed;
        }
		else
		{
            rb.velocity = new Vector2(0f, 0f);
        }
        //transform.Translate(horizontal * speed * Time.deltaTime,
        //    vertical * speed * Time.deltaTime,0);

        if (transform.localScale.x < 0 && horizontal > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); 
        }

        else if (transform.localScale.x > 0 && horizontal < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        if (horizontal != 0 || vertical != 0)
        {
            currentStatus = AIEntityStatus.Move;
        }

        else
        {
            currentStatus = AIEntityStatus.Idle;

        }
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    attack();
        //}

        #endregion

        #region Target Control

		if (Input.GetButtonDown("Fire1")&&iAttackDuring==0)
		{
            Collider2D targetCol=null;
			nearbyCollider = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			foreach (var item in nearbyCollider)
			{
                Debug.Log(item.gameObject.name);
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
                if((targetCol.transform.position - transform.position).magnitude < 6f)
				{
					Debug.LogFormat("发射了角度为{0}的子弹", Mathf.Atan2(targetCol.transform.position.y - transform.position.y, targetCol.transform.position.x - transform.position.x) * 180f / Mathf.PI);
					StartCoroutine(shotOneBullet(targetCol.transform,0.5f));
                    iAttackDuring = DataManager.Instance.Entities[entityInfo.entityDataId].AttackDuring;

                }
                Debug.Log("距离："+ targetCol.transform.position + "-"+ transform.position + "="+ (targetCol.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).magnitude );

            }
            
		}

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
