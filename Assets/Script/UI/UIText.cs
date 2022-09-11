using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
	public Text text;
	public Button exitButton;
	public bool cache = false;
	public void exit()
	{
		if (cache)
		{
			gameObject.SetActive(false);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
