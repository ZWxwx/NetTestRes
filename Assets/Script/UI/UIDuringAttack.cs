using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_STANDALONE_WIN
public class UIDuringAttack : MonoBehaviour
{
    public Vector2 offset=new Vector2(0,32);

    void Update()
    {
        
        transform.position = Input.mousePosition+(Vector3)offset;
        if(PlayerManager.Instance.currentPlayer!=null&& PlayerManager.Instance.currentPlayer.entityInfo.iAttackDuring > 0)
		{
            EntityDefine ed;
            if(!DataManager.Instance.Entities.TryGetValue(PlayerManager.Instance.currentPlayer.entityInfo.entityDataId, out ed))
			{
                return;
			}
            
            GetComponentInChildren<Slider>().value = PlayerManager.Instance.currentPlayer.entityInfo.iAttackDuring / ed.AttackDuring;

        }
		else
		{
            GetComponentInChildren<Slider>().value = 0;
        }
    }
}

#endif