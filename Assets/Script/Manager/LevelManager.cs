using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LevelManager : MonoSingleton<LevelManager>
{
	public bool isLevelOn=false;
	
	public void beginEndBattle(Team winTeam)
	{
		if (PhotonNetwork.IsMasterClient)
		{
			RaiseEventManager.Instance.SendBattleEndEvent((int)winTeam);
		}
	}
	public void endBattle(Team winTeam, float time)
	{
		StartCoroutine(endBattleCor(winTeam, time));
	}

	public IEnumerator endBattleCor(Team winTeam,float time)
	{
		
		MessageManager.Instance.AddLocalMessage((int)MessageType.Battle, "", "本局结束，获胜队是" + winTeam.ToString() + "将在" + time.ToString() + "秒后开始新的一局");
		yield return new WaitForSecondsRealtime(5f);
		PhotonNetwork.LoadLevel(1);
	}
}
