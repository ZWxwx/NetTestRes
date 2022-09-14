using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : UIWindow
{
	public Toggle isCameraFollowingToggle;
	public Toggle isConsoleEnableToggle;
	public Slider moveStickSize;
	public void Start()
	{
		moveStickSize.onValueChanged.AddListener(SettingManager.Instance.OnSettingChanged);
		isCameraFollowingToggle.onValueChanged.AddListener(SettingManager.Instance.OnSettingChanged);
	}
}
