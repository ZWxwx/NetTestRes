using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Border : MonoBehaviour
{
	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.GetComponent<BulletController>()!=null&& other.GetComponent<BulletController>().photonView.IsMine)
		{
			PhotonNetwork.Destroy(other.gameObject);
		}
		
	}
}
