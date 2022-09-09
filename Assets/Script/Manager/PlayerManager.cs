using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
public class PlayerManager : MonoSingleton<PlayerManager>
{
	public Dictionary<string, PlayerController> players = new Dictionary<string, PlayerController>();
	public PlayerController currentPlayer;
	public PhotonView photonView;
	public float autoMoneySpeed=7f;

	public GameObject killedMoney;

	public void Start()
	{
		photonView = GetComponent<PhotonView>();
		EventManager.EntityDefeated += getKilledMoney;
		EventManager.EntityDefeated += ShowKilledMoneyUI;
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
	private void getKilledMoneyPun(int money, string name)
	{
		if (players.ContainsKey(name))
		{
			players[name].money += money;
		}
	}

	private void ShowKilledMoneyUI(EntityController victim, string murderName)
	{
		PlayerController pc;
		if(!players.TryGetValue(murderName,out pc))
		{
			return;
		}
		float value;
		if (victim.isAI) {
			EntityDefine ed;
			DataManager.Instance.Entities.TryGetValue(victim.entityInfo.entityDataId, out ed);
			value = ed.Price;
		}
		else
		{
			value = 300f;
		}
		photonView.RPC("ShowKilledMoneyUIPun", RpcTarget.All,value,victim.transform.position.x, victim.transform.position.y,pc.photonView.Owner.NickName);
		
	}

	[PunRPC]
	private void ShowKilledMoneyUIPun(float value,float positionX, float positionY, string murderName)
	{
		if (PhotonNetwork.NickName == murderName)
		{
			PlayerController pc;
			if (players.TryGetValue(murderName, out pc) && pc.photonView.IsMine)
			{
				var km = Instantiate(killedMoney, new Vector2(positionX,positionY), Quaternion.identity);
				km.GetComponentInChildren<Text>().text = "+" + value.ToString();
			}
		}
	}

}
