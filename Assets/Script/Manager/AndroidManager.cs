using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidManager : MonoSingleton<AndroidManager>
{
    public Text logInfo;
    public Toggle openConsoleToggle;
	public GameObject openConsole;
	protected override void OnStart()
	{
		base.OnStart();
		openConsoleToggle.onValueChanged.AddListener(changeOpenConsoleToggle);
	}

	public void changeOpenConsoleToggle(bool value)
	{
		openConsole.SetActive(value);
	}
}
