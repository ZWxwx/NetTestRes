using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResource : MonoBehaviour
{
	public GameObject prefab;
	public GameObject obj;
	public bool cache;

	public virtual void Show()
	{
		UIManager.Instance.Show(this);
	}
}
