using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    //public static Dictionary<KeyCode, int> keyCodes = new Dictionary<KeyCode, int> { { KeyCode.Alpha1, 1 },{KeyCode.Alpha2,2}, { KeyCode.Alpha3, 3 }, };
    public EntitySpawner redSpawner;
    public EntitySpawner blueSpawner;

	private void OnEnable()
	{
        EventManager.PlayerSpawnEntity += receiveSpawnEvent;

    }

	private void OnDisable()
	{
        EventManager.PlayerSpawnEntity -= receiveSpawnEvent;
    }
	void Update()
    {
        /*
          redSpawner.spawnEntity(UIRetinueSpawnList.Instance.retinueSpawns[0].entityID);
                    PlayerManager.Instance.currentPlayer.money -= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[0].entityID].Price;
                    MessageManager.Instance.AddLocalMessage((int)MessageType.Battle, string.Format("{0}用{1}P点部署了{2}", GameManager.Instance.currentCharacter.GetComponent<PhotonView>().Owner.NickName, DataManager.Instance.Entities. UIRetinueSpawnList.Instance.retinueSpawns[0]).Price.ToString()), DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[0].entityID].Name);
         */
        if (PlayerManager.Instance.currentPlayer == null)
		{
            return;
		}
        int inputNum=-1;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inputNum = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inputNum = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inputNum = 3;
        }
        //无输入
        if (inputNum == -1)
        {
            return;
        }
        //p点不足
        if (PlayerManager.Instance.playerMoneys[PhotonNetwork.LocalPlayer.NickName] < DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[inputNum].entityID].Price)
        {
            return;
        }
        RaiseEventManager.Instance.SendPlayerSpawnEntityEvent(PhotonNetwork.LocalPlayer,PlayerManager.Instance.currentPlayer.entityInfo.teamId, inputNum);

  //      if (PlayerManager.Instance.currentPlayer.entityInfo.teamId ==(int)Team.Red)
  //      {
  //          if (Input.GetKeyDown(KeyCode.Alpha1))
  //          {
  //              inputNum = 0;
  //          }
		//	else if(Input.GetKeyDown(KeyCode.Alpha2))
		//	{
  //              inputNum = 1;
  //          }
  //          else if (Input.GetKeyDown(KeyCode.Alpha3))
  //          {
  //              inputNum = 2;
  //          }
  //          //无输入
		//	if (inputNum == -1)
		//	{
  //              return;
		//	}
  //          //p点不足
  //          if (PlayerManager.Instance.playerMoneys[PhotonNetwork.LocalPlayer.NickName] < DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[inputNum].entityID].Price)
  //          {
  //              return;
  //          }
  //          EntityDefine ed;
  //          UIRetinueSpawn urs= UIRetinueSpawnList.Instance.retinueSpawns[inputNum];
  //          redSpawner.spawnEntity(UIRetinueSpawnList.Instance.retinueSpawns[inputNum].entityID);
  //          PlayerManager.Instance.playerMoneys[PhotonNetwork.LocalPlayer.NickName] -= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[inputNum].entityID].Price;
  //          DataManager.Instance.Entities.TryGetValue(urs.entityID,out ed);
  //          MessageManager.Instance.AddLocalMessage((int)MessageType.Battle,"Battle" ,string.Format("{0}用{1}P点部署了{2}", GameManager.Instance.currentCharacter.GetComponent<PhotonView>().Owner.NickName,ed.Price.ToString(),ed.Name));

		//}
  //      else if(PlayerManager.Instance.currentPlayer.entityInfo.teamId == (int)Team.Blue)
		//{
  //          if (Input.GetKeyDown(KeyCode.Alpha1))
  //          {
  //                  inputNum = 0;
  //          }
  //          else if (Input.GetKeyDown(KeyCode.Alpha2))
  //          {
  //                  inputNum = 1;
  //          }
  //          else if (Input.GetKeyDown(KeyCode.Alpha3))
  //          {
  //                  inputNum = 2;
  //          }
  //          //无输入
  //          if (inputNum == -1)
  //          {
  //              return;
  //          }
  //          //p点不足
  //          if (PlayerManager.Instance.playerMoneys[PhotonNetwork.LocalPlayer.NickName] < DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[inputNum].entityID].Price)
  //          {
  //              return;
  //          }
  //          EntityDefine ed;
  //          UIRetinueSpawn urs = UIRetinueSpawnList.Instance.retinueSpawns[inputNum];
  //          blueSpawner.spawnEntity(UIRetinueSpawnList.Instance.retinueSpawns[inputNum].entityID);
  //          PlayerManager.Instance.playerMoneys[PhotonNetwork.LocalPlayer.NickName] -= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[inputNum].entityID].Price;
  //          DataManager.Instance.Entities.TryGetValue(urs.entityID, out ed);
  //          MessageManager.Instance.AddLocalMessage((int)MessageType.Battle, "Battle", string.Format("{0}用{1}P点部署了{2}", GameManager.Instance.currentCharacter.GetComponent<PhotonView>().Owner.NickName, ed.Price.ToString(), ed.Name));
  //      }

    }

    public void receiveSpawnEvent(Player player,int teamId,int spawnId)
	{
        UIRetinueSpawn urs = UIRetinueSpawnList.Instance.retinueSpawns[spawnId-1];
        ((Team)teamId == Team.Red ? redSpawner : blueSpawner).spawnEntity(urs.entityID);
        PlayerManager.Instance.playerMoneys[PhotonNetwork.LocalPlayer.NickName] -= DataManager.Instance.Entities[urs.entityID].Price;
        EntityDefine ed;
        DataManager.Instance.Entities.TryGetValue(urs.entityID, out ed);
        MessageManager.Instance.AddLocalMessage((int)MessageType.Battle, "Battle", string.Format("{0}用{1}P点部署了{2}", GameManager.Instance.currentCharacter.GetComponent<PhotonView>().Owner.NickName, ed.Price.ToString(), ed.Name));
    }

    public void spawnOne(UIRetinueSpawn urs,Team team)
	{
        RaiseEventManager.Instance.SendPlayerSpawnEntityEvent(PhotonNetwork.LocalPlayer, (int)team,urs.spawnID);
        /*
        (team == Team.Blue ? blueSpawner : redSpawner).spawnEntity(urs.entityID);
        PlayerManager.Instance.playerMoneys[PhotonNetwork.LocalPlayer.NickName] -= DataManager.Instance.Entities[urs.entityID].Price;
        EntityDefine ed;
        DataManager.Instance.Entities.TryGetValue(urs.entityID, out ed);
        MessageManager.Instance.AddLocalMessage((int)MessageType.Battle, "Battle", string.Format("{0}用{1}P点部署了{2}", GameManager.Instance.currentCharacter.GetComponent<PhotonView>().Owner.NickName, ed.Price.ToString(), ed.Name));
        */
    }

    
}
