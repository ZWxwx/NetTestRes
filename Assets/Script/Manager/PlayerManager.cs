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
	public PhotonView photonView;
	public float autoMoneySpeed=7f;

	public GameObject killedMoney;

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
		photonView = GetComponent<PhotonView>();
		EventManager.EntityDefeated += getKilledMoney;
		EventManager.EntityDefeated += ShowKilledMoneyUI;
		RoomManager.Instance.OnPlayerEnter += AddPlayerMoneyKV;
		RoomManager.Instance.OnPlayerLeft += DelectPlayerMoneyKV;
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

	public void getKilledMoney(EntityController victim, string name)
	{
		if (name != PhotonNetwork.NickName)
		{
			return;
		}
		playerMoneys[name] += DataManager.Instance.Entities[victim.entityInfo.entityDataId].Price / 4;
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

	private void ShowKilledMoneyUI(EntityController victim, string murderName)
	{
		if (murderName != PhotonNetwork.NickName)
		{
			return;
		}

		float value;
		if (victim.isAI) {
			EntityDefine ed;
			DataManager.Instance.Entities.TryGetValue(victim.entityInfo.entityDataId, out ed);
			value = ed.Price/4;
		}
		else
		{
			value = 150f;
		}

		var km = Instantiate(killedMoney, new Vector2(victim.transform.position.x, victim.transform.position.y), Quaternion.identity);
		km.GetComponentInChildren<Text>().text = "+" + value.ToString();
		
	}
}
