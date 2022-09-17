using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class GameManager : MonoSingleton<GameManager>
{
    
    [Header("选择队伍")]
    public GameObject chosingBoard;
    public GameObject readyButton;
    public UITeamButton redButton;
    public UITeamButton blueButton;
    public List<UITeamButton> teamButtons;
    public Team selectedTeam;
    [Header("其他")]
    public GameObject currentCharacter;
    public GameObject respawnButton;
    [Header("关卡系统")]
    public AIEntityController redTower;
    public AIEntityController blueTower;

	private void Start()
	{
        foreach (var item in GameManager.Instance.teamButtons)
        {
            item.Selected = false;
        }
    }
	public void readyToPlay()
	{
		if (selectedTeam == Team.None)
		{
            return;
		}
        chosingBoard.SetActive(false);
        InitPlayer();
    }
    
    public void ResetRespawnButton(string name)
	{
        if (name.Equals(PhotonNetwork.NickName))
        {
            respawnButton.GetComponent<Button>().interactable = true;
        }
    }
    public void Respawn()
	{
        InitPlayer();
    }

    public void InitPlayer()
	{
        GameObject player=null;
        EntitySpawner esp=selectedTeam==Team.Red?SpawnManager.Instance.redSpawner: SpawnManager.Instance.blueSpawner;
		if (esp == null)
		{
            return;
		}
        player=esp.spawnPlayer(102).gameObject;
        //player.GetComponent<PhotonView>().RPC("ReceiveInitialData", RpcTarget.All, 102,(int)selectedTeam,DataManager.Instance.Entities[102].MaxHealth);
        currentCharacter = player;
        respawnButton.GetComponent<Button>().interactable = false;
        //player.GetComponent<PlayerController>().entityInfo.entityDataId = 102;
        UIPlayerInfo.Instance.setPlayer(player.GetComponent<PlayerController>());
        PlayerManager.Instance.currentPlayer = player.GetComponent<PlayerController>();
    }

}
