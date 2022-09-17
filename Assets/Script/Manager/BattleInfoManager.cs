using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BattleInfoManager : MonoSingleTonPun<BattleInfoManager>
{
	public Dictionary<string, Vector3> playerBattleInfos = new Dictionary<string, Vector3>();

	private void OnEnable()
	{
		base.OnEnable();
		RaiseEventManager.Instance.SendBattleInfoToNew(PhotonNetwork.LocalPlayer);
	}
	protected override void OnStart()
    {
		playerBattleInfos.Add(PhotonNetwork.NickName, Vector3.zero);
		EventManager.EntityDefeated += this.releaseDefeatedInfo;
		EventManager.EntityDefeated += this.refreshPlayerBattleInfo;
		EventManager.PlayerEnter += AddPlayerBattleInfos;
		EventManager.PlayerLeft+= DelectPlayerBattleInfos;
		EventManager.SendBattleInfoToNewReq += SendInfoToNew;
	}

	void AddPlayerBattleInfos(Player player)
	{
		if (!playerBattleInfos.ContainsKey(player.NickName))
		{
			playerBattleInfos.Add(player.NickName, Vector3.zero);
		}
	}

	[PunRPC]
	void ReceiveInitialOneBattleInfo(string receiver,string playerName,Vector3 info)
	{
		if(PhotonNetwork.NickName== receiver)
		{
			if (!playerBattleInfos.ContainsKey(playerName))
			{
				playerBattleInfos.Add(playerName, info);
			}
		}
	}

	void SendInfoToNew(Player newPlayer)
	{
		if (newPlayer.NickName == PhotonNetwork.NickName)
		{
			return;
		}
		Vector3 info;
		if (playerBattleInfos.TryGetValue(PhotonNetwork.NickName, out info)) {
			photonView.RPC("ReceiveInitialOneBattleInfo", RpcTarget.All, newPlayer.NickName,PhotonNetwork.NickName, info);
		}
	}

	void DelectPlayerBattleInfos(Player player)
	{
		if (playerBattleInfos.ContainsKey(player.NickName))
		{
			playerBattleInfos.Remove(player.NickName);
		}
	}

	public override void OnJoinedRoom()
	{
		base.OnJoinedRoom();
	}


	void releaseDefeatedInfo(string victimName, int viewID, int entityID, int teamID, bool isVictimAI, Vector2 position, string murdererName)
	{
        if (!isVictimAI)
        {
            UIBattleInfo.Instance.photonView.RPC("setText", Photon.Pun.RpcTarget.All, string.Format("{0}±ª{1}ﬂŸ¡À", victimName, murdererName));
        }
	}

    void refreshPlayerBattleInfo(string victimName, int viewID, int entityID, int teamID, bool isVictimAI, Vector2 position, string murdererName)
	{
		Vector3 battleInfos;
		if (playerBattleInfos.TryGetValue(murdererName!=null?murdererName:"NullName",out battleInfos))
		{
			if (!isVictimAI)
			{
				playerBattleInfos[murdererName] = new Vector3(playerBattleInfos[murdererName].x + 1, playerBattleInfos[murdererName].y, playerBattleInfos[murdererName].z);
			}
			else
			{
				playerBattleInfos[murdererName] = new Vector3(playerBattleInfos[murdererName].x, playerBattleInfos[murdererName].y, playerBattleInfos[murdererName].z + 1);
			}
		}
		if (!isVictimAI)
		{
			playerBattleInfos[victimName] = new Vector3(playerBattleInfos[victimName].x, playerBattleInfos[victimName].y + 1, playerBattleInfos[victimName].z);
		}
		foreach(var kv in playerBattleInfos)
		{
			bool flag=false;
			foreach(var player in PhotonNetwork.PlayerList)
			{
				if (player.NickName.Equals(kv.Key))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				playerBattleInfos.Remove(kv.Key);
			}
		}
	}

	//[PunRPC]

	//public void addPlayerKilled(string player)
	//{
	//	if (PlayerManager.Instance.players[player] != null)
	//	{
	//		PlayerManager.Instance.players[player].playerBattleInfo.playerKilled += 1;
	//	}
	//}

	//[PunRPC]
	//public void addAIKilled(string player)
	//{
	//	if (PlayerManager.Instance.players[player] != null)
	//	{
	//		PlayerManager.Instance.players[player].playerBattleInfo.aiKilled += 1;
	//	}
	//}

	//[PunRPC]
	//public void addDeath(string player)
	//{
	//	if (PlayerManager.Instance.players[player] != null)
	//	{
	//		PlayerManager.Instance.players[player].playerBattleInfo.death += 1;
	//	}
		
	//}
}
