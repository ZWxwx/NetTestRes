using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class EventManager : MonoBehaviourPunCallbacks
{
    public static Action<BulletController,EntityController> EntityBeHit= EntityBeHitHandler;
	[Tooltip("int viewID,int entityID,int teamID,bool isVictimAI,")]
	public static Action<string,int ,int ,int, bool, Vector2, string> EntityDefeated = EntityDefeatedHandler;
	public static Action<Player> PlayerEnter = PlayerEnterHandler;
	public static Action<Player> PlayerLeft = PlayerLeftHandler;
	public static Action<int> BattleEnd = BattleEndHandler;
	public static Action<Player> SendBattleInfoToNewReq = SendBattleInfoToNewHandler;
	public static Action<Player,int,int> PlayerSpawnEntity = PlayerSpawnEntityHandler;

	public static void PlayerSpawnEntityHandler(Player player, int teamId, int spawnId)
	{
		
	}

	private static void SendBattleInfoToNewHandler(Player obj)
	{
	}

	public static void EntityBeHitHandler(BulletController bullet, EntityController entity)
	{
		if (bullet.photonView.IsMine)
		{
			if (bullet.bulletInfo.leftDamageTime <= 0)
			{
				PhotonNetwork.Destroy(bullet.gameObject);
			}
		}
	}

	public static void EntityDefeatedHandler(string victimName, int viewID, int entityID, int teamID, bool isVictimAI, Vector2 position, string murderer)
	{
		Debug.Log("EntityDefeatedHandler is Running");

		#region Baned
		//if (victim.photonView.IsMine)
		//{
		//	victim.entityInfo.transformBeforeDefeatedForward = (int)victim.transform.localScale.x;
		//	victim.entityInfo.transformBeforeDefeatedPosition = victim.transform.position;
		//	GameObject body = PhotonNetwork.Instantiate("DefeatedBody", victim.entityInfo.transformBeforeDefeatedPosition, Quaternion.identity);
		//	body.GetComponent<PhotonView>().RPC("ReceiveInitialData", RpcTarget.All, DataManager.Instance.Entities[victim.entityInfo.entityDataId].DefeatedAnimId);
		//	body.transform.localScale = new Vector3(victim.entityInfo.transformBeforeDefeatedForward, 1, 1);
		//	if (PhotonNetwork.InRoom)
		//	{
		//		if (victim.Equals((EntityController)PlayerManager.Instance.currentPlayer))
		//		{
		//			GameManager.Instance.ResetRespawnButton(victim.photonView.Owner.NickName);
		//		}
		//		PhotonNetwork.Destroy(victim.gameObject);
		//	}


		//}
		#endregion
	}

	public static void PlayerLeftHandler(Player newPlayer)
	{

	}
	public static void PlayerEnterHandler(Player newPlayer)
	{

	}

	public static void BattleEndHandler(int winTeamId)
	{
		LevelManager.Instance.endBattle((Team)winTeamId, 5f);
	}

}
