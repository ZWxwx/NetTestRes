using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BattleInfoManager : MonoSingleTonPun<BattleInfoManager>
{
	public Dictionary<string, Vector3> playerBattleInfos = new Dictionary<string, Vector3>();
	protected override void OnStart()
    {
		playerBattleInfos.Add(PhotonNetwork.NickName, Vector3.zero);
		EventManager.EntityDefeated += this.releaseDefeatedInfo;
		EventManager.EntityDefeated += this.refreshPlayerBattleInfo;
		RoomManager.Instance.OnPlayerEnter += AddPlayerBattleInfos;
		RoomManager.Instance.OnPlayerLeft += DelectPlayerBattleInfos;
		RoomManager.Instance.OnPlayerEnter += SendInfoToNew;
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

	

	void releaseDefeatedInfo(EntityController victim,string murdererName)
	{
        if (!victim.isAI)
        {
            UIBattleInfo.Instance.photonView.RPC("setText", Photon.Pun.RpcTarget.All, string.Format("{0}±ª{1}ﬂŸ¡À", victim.photonView.Owner.NickName, murdererName));
        }
	}

    void refreshPlayerBattleInfo(EntityController victim, string murdererName)
	{
		Vector3 battleInfos;
		if (playerBattleInfos.TryGetValue(murdererName,out battleInfos))
		{
			if (!victim.isAI)
			{
				playerBattleInfos[murdererName] = new Vector3(playerBattleInfos[murdererName].x + 1, playerBattleInfos[murdererName].y, playerBattleInfos[murdererName].z);
			}
			else
			{
				playerBattleInfos[murdererName] = new Vector3(playerBattleInfos[murdererName].x, playerBattleInfos[murdererName].y, playerBattleInfos[murdererName].z + 1);
			}
		}
		if (!victim.isAI)
		{
			playerBattleInfos[victim.photonView.Owner.NickName] = new Vector3(playerBattleInfos[victim.photonView.Owner.NickName].x, playerBattleInfos[victim.photonView.Owner.NickName].y + 1, playerBattleInfos[victim.photonView.Owner.NickName].z);
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
