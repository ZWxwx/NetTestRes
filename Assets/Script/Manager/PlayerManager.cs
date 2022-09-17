using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
public class PlayerManager : MonoSingleTonPun<PlayerManager> {
	const int initialMoney= 100;
	public Dictionary<string, int> playerMoneys = new Dictionary<string, int>();
	public PlayerController currentPlayer;
	public float autoMoneySpeed=7f;

	public GameObject killedMoney;

	public void Update()
	{
		if (currentPlayer == null&&!GameManager.Instance.chosingBoard.activeInHierarchy) {
			GameManager.Instance.ResetRespawnButton(PhotonNetwork.NickName);
		}
	}
	public void AddPlayerMoneyKV(Player player)
	{
		if (!playerMoneys.ContainsKey(player.NickName))
		{
			playerMoneys.Add(player.NickName, initialMoney);
		}
	}
	public void DelectPlayerMoneyKV(Player player)
	{
		if (playerMoneys.ContainsKey(player.NickName))
		{
			playerMoneys.Remove(player.NickName);
		}
	}

	protected override void OnStart()
	{
		EventManager.EntityDefeated += getKilledMoney;
		EventManager.EntityDefeated += ShowKilledMoneyUI;
		EventManager.PlayerEnter += AddPlayerMoneyKV;
		EventManager.PlayerLeft += DelectPlayerMoneyKV;
		playerMoneys.Add(PhotonNetwork.NickName, initialMoney);
		StartCoroutine(autoMoney());
	}

	public void SetInitailData()
	{

	}

	[PunRPC]
	public void ReceiveInitialData(string playerName)
	{
		if (PhotonNetwork.NickName != playerName)
		{
			return;
		}
		//InitPlayers();
		SetInitailData();
	}

	//public void InitPlayers()
	//{
	//	players.Clear();
	//	PlayerController[] pcs = FindObjectsOfType<PlayerController>();
	//	if (pcs != null)
	//	{
	//		foreach (var player in pcs)
	//		{
	//			players.Add(player.photonView.Owner.NickName, player);
	//		}
	//	}
	//}

	public IEnumerator autoMoney()
	{
		while (true)
		{
			if (Instance.playerMoneys.ContainsKey(PhotonNetwork.LocalPlayer.NickName))
			{
				Instance.playerMoneys[PhotonNetwork.LocalPlayer.NickName] += (int)autoMoneySpeed;
			}
			yield return new WaitForSecondsRealtime(1f);
		}
	}

	public void getKilledMoney(string victimName, int viewID, int entityID, int teamID, bool isVictimAI, Vector2 position, string name)
	{
		if (name != PhotonNetwork.NickName)
		{
			return;
		}
		playerMoneys[name] += DataManager.Instance.Entities[entityID].Price / 4;
		//photonView.RPC("getKilledMoneyPun", RpcTarget.All, DataManager.Instance.Entities[victim.entityInfo.entityDataId].Price / 4, name);
	}

	//[PunRPC]
	//private void getKilledMoneyPun(int money, string name)
	//{
	//	if (playerMoneys.ContainsKey(name))
	//	{
	//		playerMoneys[name] += money;
	//	}
	//}

	private void ShowKilledMoneyUI(string victimName, int viewID, int entityID, int teamID, bool isVictimAI, Vector2 position, string murderName)
	{
		if (murderName != PhotonNetwork.NickName)
		{
			return;
		}

		float value;
		if (isVictimAI) {
			EntityDefine ed;
			DataManager.Instance.Entities.TryGetValue(entityID, out ed);
			value = ed.Price/4;
		}
		else
		{
			value = 150f;
		}

		var km = Instantiate(killedMoney,position, Quaternion.identity);
		km.GetComponentInChildren<Text>().text = "+" + value.ToString();
		
	}
}
