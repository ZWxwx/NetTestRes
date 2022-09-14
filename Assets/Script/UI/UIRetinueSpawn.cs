using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;

[RequireComponent(typeof(Button))]
public class UIRetinueSpawn : MonoBehaviour
{
	public int spawnID;
	public int entityID;
	public Image entityImage;
	public Text nameText;
	public Text ID;
	public Text PriceText;
	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(clickToSpawn);
		refreshInfo();
	}
	public void refreshInfo()
	{
		this.ID.text = spawnID.ToString();
		this.nameText.text = DataManager.Instance.Entities[entityID].Name;
		this.PriceText.text = DataManager.Instance.Entities[entityID].Price.ToString();
		this.entityImage.overrideSprite = DataManager.Instance.EntityImage[DataManager.Instance.Entities[entityID].ImageID];

	}

	public void clickToSpawn()
	{
		//p点不足
		if (PlayerManager.Instance.playerMoneys[PhotonNetwork.LocalPlayer.NickName] < DataManager.Instance.Entities[entityID].Price)
		{
			MessageManager.Instance.AddLocalMessage((int)MessageType.Battle, "", "你没有足够P点");
			return;
		}
		if (PlayerManager.Instance.currentPlayer != null)
		{
			SpawnManager.Instance.spawnOne(this, (Team)PlayerManager.Instance.currentPlayer.entityInfo.teamId);
			
		}
		else
		{
			MessageManager.Instance.AddLocalMessage((int)MessageType.Battle, "", "你无法在被击败状态部署");
		}
	}
}
