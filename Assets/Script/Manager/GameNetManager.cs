using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class GameNetManager : MonoBehaviourPunCallbacks
{
    #region Private Methods


    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", 1);
        PhotonNetwork.LoadLevel("Room for " +1);
    }


	#endregion

	#region Photon Callbacks
	#endregion

}
