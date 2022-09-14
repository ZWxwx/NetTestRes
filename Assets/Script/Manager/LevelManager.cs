using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LevelManager : MonoSingleton<LevelManager>
{
	public bool isLevelOn=false;
    void Update()
    {
		if (isLevelOn)
		{
			if (GameManager.Instance.redTower == null)
			{
				StartCoroutine(endBattle(Team.Blue, 5f));
			}
			else if (GameManager.Instance.blueButton == null)
			{
				StartCoroutine(endBattle(Team.Red, 5f));
			}
		}
    }

	public IEnumerator endBattle(Team winTeam,float time)
	{
		
		MessageManager.Instance.AddLocalMessage((int)MessageType.Battle, "", "本局结束，获胜队是" + winTeam.ToString() + "将在" + time.ToString() + "秒后开始新的一局");
		yield return new WaitForSecondsRealtime(5f);
		if (PhotonNetwork.IsMasterClient)
		{
			PhotonNetwork.LoadLevel(1);
		}
	}
}
