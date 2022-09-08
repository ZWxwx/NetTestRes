using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PlayerManager : MonoSingleton<PlayerManager>
{
	public Dictionary<string, PlayerController> players = new Dictionary<string, PlayerController>();
	public PlayerController currentPlayer;
	public PhotonView photonView;
	public float autoMoneySpeed=7f;
	public void Start()
	{
		photonView = GetComponent<PhotonView>();
		EventManager.EntityDefeated += getKilledMoney;
		StartCoroutine(autoMoney());
	}

	public IEnumerator autoMoney()
	{
		while (true)
		{
			if (currentPlayer != null)
			{
				currentPlayer.money += (int)autoMoneySpeed;
			}
			yield return new WaitForSecondsRealtime(1f);
		}
	}

	public void getKilledMoney(EntityController victim, string name)
	{
		photonView.RPC("getKilledMoneyPun", RpcTarget.All, DataManager.Instance.Entities[victim.entityInfo.entityDataId].Price / 4, name);
	}

	[PunRPC]
	public void getKilledMoneyPun(int money, string name)
	{
		if (players.ContainsKey(name))
		{
			players[name].money += money;
		}
	}


}
