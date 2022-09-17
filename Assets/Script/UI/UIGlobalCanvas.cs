using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGlobalCanvas : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnEnable()
	{
		DontDestroyOnLoad(this.gameObject);
	}
}
