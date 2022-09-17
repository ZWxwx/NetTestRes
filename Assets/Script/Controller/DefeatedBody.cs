using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DefeatedBody : MonoBehaviourPunCallbacks,IPunObservable
{
	[SerializeField]
	private SpriteRenderer sr;
	[Header("决定是谁的战败动画")]
	public int DefeatedAnimId;
	public void Start()
	{
		StartCoroutine(removeBodyCor());
		sr.GetComponent<Animator>().runtimeAnimatorController = DataManager.Instance.Animator[DefeatedAnimId];
		sr.GetComponent<Animator>().SetTrigger("Defeated");
	}

	[PunRPC]
	public void ReceiveInitialData(int DefeatedAnimId,float xToward)
	{
		this.DefeatedAnimId = DefeatedAnimId;
		transform.localScale = new Vector3(xToward,transform.localScale.y,transform.localScale.z);
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(DefeatedAnimId);
		}
		else
		{
			DefeatedAnimId = (int)stream.ReceiveNext();
		}
	}

	public IEnumerator removeBodyCor()
	{
		
		yield return new WaitForSeconds(3f);
		while (sr.color.a > 0)
		{
			sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.04f);
			yield return new WaitForSeconds(0.2f);
		}
		if (photonView.IsMine)
		{
			PhotonNetwork.Destroy(gameObject);
		}
	}
}
