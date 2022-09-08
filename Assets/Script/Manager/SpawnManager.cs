using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //public static Dictionary<KeyCode, int> keyCodes = new Dictionary<KeyCode, int> { { KeyCode.Alpha1, 1 },{KeyCode.Alpha2,2}, { KeyCode.Alpha3, 3 }, };
    public EntitySpawner redSpawner;
    public EntitySpawner blueSpawner;
    void Update()
    {
		if (PlayerManager.Instance.currentPlayer == null)
		{
            return;
		}
		if (PlayerManager.Instance.currentPlayer.entityInfo.teamId ==(int)Team.Red)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (PlayerManager.Instance.currentPlayer.money >= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[0].entityID].Price) {
                    redSpawner.spawnEntity(UIRetinueSpawnList.Instance.retinueSpawns[0].entityID);
                    PlayerManager.Instance.currentPlayer.money -= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[0].entityID].Price;
                }

            }
			else if(Input.GetKeyDown(KeyCode.Alpha2))
			{
                if (PlayerManager.Instance.currentPlayer.money >= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[1].entityID].Price)
                {
                    redSpawner.spawnEntity(UIRetinueSpawnList.Instance.retinueSpawns[1].entityID);
                    PlayerManager.Instance.currentPlayer.money -= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[1].entityID].Price;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (PlayerManager.Instance.currentPlayer.money >= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[2].entityID].Price)
                {
                    redSpawner.spawnEntity(UIRetinueSpawnList.Instance.retinueSpawns[2].entityID);
                    PlayerManager.Instance.currentPlayer.money -= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[2].entityID].Price;
                }
            }

        }
        else if(PlayerManager.Instance.currentPlayer.entityInfo.teamId == (int)Team.Blue)
		{
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (PlayerManager.Instance.currentPlayer.money >= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[0].entityID].Price)
                {
                    blueSpawner.spawnEntity(UIRetinueSpawnList.Instance.retinueSpawns[0].entityID);
                    PlayerManager.Instance.currentPlayer.money -= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[0].entityID].Price;
                }

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (PlayerManager.Instance.currentPlayer.money >= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[1].entityID].Price)
                {
                    blueSpawner.spawnEntity(UIRetinueSpawnList.Instance.retinueSpawns[1].entityID);
                    PlayerManager.Instance.currentPlayer.money -= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[1].entityID].Price;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (PlayerManager.Instance.currentPlayer.money >= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[2].entityID].Price)
                {
                    blueSpawner.spawnEntity(UIRetinueSpawnList.Instance.retinueSpawns[2].entityID);
                    PlayerManager.Instance.currentPlayer.money -= DataManager.Instance.Entities[UIRetinueSpawnList.Instance.retinueSpawns[2].entityID].Price;
                }
            }
        }

    }
}
