using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseEventManager : MonoSingleton<RaiseEventManager>,IOnEventCallback
{
	public const byte SendEntityDefeatedEventCode = 1;
	public const byte SendPlayerEnterEventCode = 2;
	public const byte SendPlayerLeftEventCode = 3;
	public const byte SendBattleEndEventCode = 4;
	public const byte SendBattleInfoToNewCode = 5;
	public const byte SendPlayerSpawnEntityCode = 6;
	private void OnEnable()
	{
		PhotonNetwork.AddCallbackTarget(this);
	}

	private void OnDisable()
	{
		PhotonNetwork.RemoveCallbackTarget(this);
	}
	public void OnEvent(EventData photonEvent)
	{
		object[] content;
		switch (photonEvent.Code)
		{
			case SendEntityDefeatedEventCode:
				content = (object[])photonEvent.CustomData;
				if (EventManager.EntityDefeated != null) { 
					EventManager.EntityDefeated((string)content[0], (int)content[1], (int)content[2],(int)content[3],(bool)content[4],(Vector2)content[5],(string)content[6]); 
				}
				break;
			case SendPlayerEnterEventCode:
				content = (object[])photonEvent.CustomData;
				if (EventManager.PlayerEnter != null)
				{
					EventManager.PlayerEnter((Player)content[0]);
				}
				break;
			case SendPlayerLeftEventCode:
				content = (object[])photonEvent.CustomData;
				if (EventManager.PlayerLeft != null)
				{
					EventManager.PlayerLeft((Player)content[0]);
				}
				break;
				
			case SendBattleEndEventCode:
				content = (object[])photonEvent.CustomData;
				if (EventManager.BattleEnd != null)
				{
					EventManager.BattleEnd((int)content[0]);
				}
				break;
			case SendBattleInfoToNewCode:
				content = (object[])photonEvent.CustomData;
				if (EventManager.SendBattleInfoToNewReq != null)
				{
					EventManager.SendBattleInfoToNewReq((Player)content[0]);
				}
				break;
			case SendPlayerSpawnEntityCode:
				content = (object[])photonEvent.CustomData;
				if (EventManager.PlayerSpawnEntity != null)
				{
					EventManager.PlayerSpawnEntity((Player)content[0],(int)content[1],(int)content[2]);
				}
				break;
			default:
				break;
		}
	}

	public void SendEntityDefeatedEvent(string victimName, int viewID,int entityID,int teamID,bool isVictimAI, Vector2 position, string murderName)
	{
		//EntityInfo entityInfo=new EntityInfo();
		//content包含被击杀者的entityDataID,位置信息(Vector3)，teamId,击杀者姓名
		object[] content = new object[] { victimName, viewID,entityID, teamID, isVictimAI, position, murderName };
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
		PhotonNetwork.RaiseEvent(SendEntityDefeatedEventCode, content, raiseEventOptions, SendOptions.SendReliable);
	}

	public void SendPlayerEnterEvent(Player newPlayer)
	{
		object[] content = new object[] { newPlayer};
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
		PhotonNetwork.RaiseEvent(SendPlayerEnterEventCode, content, raiseEventOptions, SendOptions.SendReliable);
	}

	public void SendPlayerLeftEvent(Player newPlayer)
	{
		object[] content = new object[] { newPlayer };
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
		PhotonNetwork.RaiseEvent(SendPlayerLeftEventCode, content, raiseEventOptions, SendOptions.SendReliable);
	}

	public void SendBattleEndEvent(int winTeamId)
	{
		object[] content = new object[] { winTeamId };
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
		PhotonNetwork.RaiseEvent(SendBattleEndEventCode, content, raiseEventOptions, SendOptions.SendReliable);
	}
	
	public void SendBattleInfoToNew(Player newPlayer)
	{
		object[] content = new object[] { newPlayer };
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
		PhotonNetwork.RaiseEvent(SendBattleInfoToNewCode, content, raiseEventOptions, SendOptions.SendReliable);
	}

	public void SendPlayerSpawnEntityEvent(Player player,int teamId,int spawnId)
	{
		object[] content = new object[] { player, teamId,spawnId };
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
		PhotonNetwork.RaiseEvent(SendPlayerSpawnEntityCode, content, raiseEventOptions, SendOptions.SendReliable);
	}
}
