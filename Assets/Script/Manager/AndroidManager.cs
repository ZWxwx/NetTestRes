using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidManager : MonoSingleton<AndroidManager>
{
    public Text logInfo;
	public GameObject openConsole;
	protected override void OnStart()
	{
		base.OnStart();
		changeOpenConsoleToggle();
		SettingManager.Instance.onSettingChanged += changeOpenConsoleToggle;
	}

	public void changeOpenConsoleToggle()
	{
		openConsole.SetActive(SettingManager.Instance.IsConsoleEnable);
	}

	public void OnDestroy()
	{
		SettingManager.Instance.onSettingChanged -= changeOpenConsoleToggle;
	}
}
