using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerBattleInfoItem : MonoBehaviour
{
	public Text playerName;
	public Text playerKilled;
	public Text death;
	public Text aiKilled;

	public void refreshInfo()
	{
		if (BattleInfoManager.Instance.playerBattleInfos[playerName.text] != null) {
			playerKilled.text = ((int)BattleInfoManager.Instance.playerBattleInfos[playerName.text].x).ToString();
			death.text = ((int)BattleInfoManager.Instance.playerBattleInfos[playerName.text].y).ToString();
			aiKilled.text = ((int)BattleInfoManager.Instance.playerBattleInfos[playerName.text].z).ToString(); ;
		}
	}
}
