using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class BulletController : MonoBehaviourPunCallbacks
{

    public BulletInfo bulletInfo;
    public Rigidbody2D rb;
       
    public float angle;
    public float Angle
	{
		get
		{
            return angle;
		}
		set
		{
            angle = value;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
            rb.velocity =  bulletInfo.speed* new Vector2(Mathf.Cos(angle*Mathf.PI/180), Mathf.Sin(angle * Mathf.PI / 180)).normalized;
        }
    }

	private void Start()
	{
        
	}

    public void setInitailData()
	{
        GetComponent<SpriteRenderer>().sprite = DataManager.Instance.BulletSprite[DataManager.Instance.Bullets[bulletInfo.bulletDataId].SpriteId];
        this.bulletInfo.speed = DataManager.Instance.Bullets[bulletInfo.bulletDataId].Speed;
    }

    [PunRPC]
    public void ReceiveInitialData(int bulletDataId,string ownerName)
	{
        this.bulletInfo.ownerName= ownerName;
        this.bulletInfo.bulletDataId = bulletDataId;
        
        setInitailData();
	}
}
