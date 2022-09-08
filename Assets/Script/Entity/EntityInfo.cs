using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class EntityInfo : MonoBehaviour, IPunObservable
{
	#region Public Fields
	public int entityDataId;
	public EntityController entity;
	public float maxHealth;
	public float CurrentHealth;
	public int teamId;
	[Header("发射投掷物的位置，常用于远程单位")]
	public Vector2 attackPosition;
	#endregion

	#region temp

	public Vector3 transformBeforeDefeatedPosition;
	public int transformBeforeDefeatedForward;

	#endregion
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{

		if (stream.IsWriting)
		{
			stream.SendNext(teamId);
			stream.SendNext(CurrentHealth);
			stream.SendNext(entityDataId);
		}
		else
		{
			this.teamId = (int)stream.ReceiveNext();
			this.CurrentHealth = (float)stream.ReceiveNext();
			this.entityDataId = (int)stream.ReceiveNext();
		}
	}

}
