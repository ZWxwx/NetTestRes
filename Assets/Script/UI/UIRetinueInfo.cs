using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRetinueInfo : UIWindow
{
	public GameObject uriPrefab;
	public Transform itemTransform;
	List<UIRetinueInfoItem> uiRetinueInfoItems=new List<UIRetinueInfoItem>();

	public void Awake()
	{
		refreshItems();
	}
	public void refreshItems()
	{
		if (uiRetinueInfoItems != null)
		{
			uiRetinueInfoItems.Clear();
		}
		foreach (var uri in DataManager.Instance.Entities)
		{
			GameObject go = Instantiate(uriPrefab, itemTransform);
			go.GetComponent<UIRetinueInfoItem>().entityId = uri.Key;
			uiRetinueInfoItems.Add(go.GetComponent<UIRetinueInfoItem>());
		}
	}
}
