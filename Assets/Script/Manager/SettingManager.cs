using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoSingleton<SettingManager>
{
	public Action onSettingChanged;
	public UISetting uiSetting;
	public bool IsCameraFollowing
	{
		get
		{
			return uiSetting.isCameraFollowingToggle.isOn;
		}
	}
	public bool IsConsoleEnable
	{
		get
		{
			return uiSetting.isConsoleEnableToggle.isOn;
		}
	}

	
	public float moveStickSizePosition
	{
		get
		{
			return uiSetting.moveStickSize.value;
		}
	}

	public void OnSettingChanged(bool value)
	{
		onSettingChanged();
	}

	public void OnSettingChanged(float value)
	{
		onSettingChanged();
	}
}
