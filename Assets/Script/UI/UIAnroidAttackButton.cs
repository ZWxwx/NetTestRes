using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnroidAttackButton : MonoBehaviour
{
    
    void Update()
    {
        if (PlayerManager.Instance.currentPlayer != null) {
            this.GetComponent<Image>().fillAmount = 1-PlayerManager.Instance.currentPlayer.entityInfo.iAttackDuring / DataManager.Instance.Entities[PlayerManager.Instance.currentPlayer.entityInfo.entityDataId].AttackDuring;
        }
    }
}
