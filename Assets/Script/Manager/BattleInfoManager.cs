using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfoManager : MonoSingleton<BattleInfoManager>
{
    void Start()
    {
        EventManager.EntityDefeated += this.releaseDefeatedInfo;
    }

    void releaseDefeatedInfo(EntityController victim,string murdererName)
	{
        if (!victim.isAI)
        {
            UIBattleInfo.Instance.photonView.RPC("setText", Photon.Pun.RpcTarget.All, string.Format("{0}±ª{1}ﬂŸ¡À", victim.photonView.Owner.NickName, murdererName));
        }
	}

}
