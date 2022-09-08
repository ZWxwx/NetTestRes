using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		refreshInfo();
	}
	public void refreshInfo()
	{
		this.ID.text = spawnID.ToString();
		this.nameText.text = DataManager.Instance.Entities[entityID].Name;
		this.PriceText.text = DataManager.Instance.Entities[entityID].Price.ToString();
		this.entityImage.overrideSprite = DataManager.Instance.EntityImage[DataManager.Instance.Entities[entityID].ImageID];

	}
}
