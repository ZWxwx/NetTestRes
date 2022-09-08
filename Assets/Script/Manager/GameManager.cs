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
    public void readyToPlay()
	{
        chosingBoard.SetActive(false);
        //GameObject player = PhotonNetwork.Instantiate("Player", new Vector3(1, 1, 0), Quaternion.identity, 0);
        //player.GetComponent<EntityController>().entityInfo.teamId = (int)selectedTeam;
        //currentCharacter = player;
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
        GameObject player = PhotonNetwork.Instantiate("Player", new Vector3(1, 1, 0), Quaternion.identity, 0);
        player.GetComponent<EntityController>().entityInfo.teamId = (int)selectedTeam;
        currentCharacter = player;
        respawnButton.GetComponent<Button>().interactable = false;
        UIPlayerInfo.Instance.setPlayer(player.GetComponent<PlayerController>());
        PlayerManager.Instance.players.Add(PhotonNetwork.NickName, player.GetComponent<PlayerController>());
        PlayerManager.Instance.currentPlayer = player.GetComponent<PlayerController>();
    }

    public void onPlayerJoined(PlayerController pc) {

    }

}
