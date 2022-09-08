using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public abstract class UISelectedItem : MonoBehaviour, ISelectHandler
{
	public bool selected;
	public bool Selected
	{
		get { return selected; }
		set
		{
			selected = value;
			Select(value);
			//this.background.overrideSprite = selected ? selectedBg : normalBg;
		}
	}
	public void OnSelect(BaseEventData eventData)
	{
		this.Selected = true;
	}

	public abstract void Select(bool f);

	//public void SetShopItem(int id, ShopItemDefine shopItem, UIShop owner)
	//{
	//	this.shop = owner;
	//	this.ShopItemID = id;
	//	this.ShopItem = shopItem;
	//	this.item = DataManager.Instance.Items[this.ShopItem.ItemID];

	//	this.title.text = this.item.Name;
	//	this.count.text = ShopItem.Count.ToString();
	//	this.price.text = ShopItem.price.ToString();
	//	this.icon.overrideSprite = Resloader.Load<Sprite>(item.Icon);
	//}
}