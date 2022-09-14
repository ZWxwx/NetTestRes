using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletInfo : MonoBehaviourPunCallbacks,IPunObservable
{
    public int bulletDataId = -1;
    [Tooltip("子弹发射者")]
    public string ownerName;
    public int teamId = -1;
    public float speed;
    [Tooltip("剩余可造成伤害次数")]
    public int leftDamageTime=1;
    #region IPunObservableImplement

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(bulletDataId);
            stream.SendNext(teamId);
            stream.SendNext(leftDamageTime);
        }
        else
        {
            this.bulletDataId = (int)stream.ReceiveNext();
            this.teamId = (int)stream.ReceiveNext();
            this.leftDamageTime = (int)stream.ReceiveNext();
        }
    }

    #endregion
}
