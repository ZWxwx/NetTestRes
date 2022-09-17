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
		
		MessageManager.Instance.AddLocalMessage((int)MessageType.Battle, "", "���ֽ�������ʤ����" + winTeam.ToString() + "����" + time.ToString() + "���ʼ�µ�һ��");
		yield return new WaitForSecondsRealtime(5f);
		PhotonNetwork.LoadLevel(1);
	}
}
