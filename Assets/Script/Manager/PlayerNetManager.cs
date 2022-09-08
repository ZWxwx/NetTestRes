using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PlayerNetManager : MonoBehaviourPunCallbacks, IPunObservable
{
	#region IPunObservable Implements
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		
	}

	#endregion 
}
