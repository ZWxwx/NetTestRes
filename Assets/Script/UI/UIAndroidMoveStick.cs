using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoystick))]
public class UIAndroidMoveStick : MonoBehaviour
{
	public Vector2 SizeRange;

	public void Start()
	{
	}
	public void Update()
	{
		transform.localScale = new Vector3(SizeRange.x + SettingManager.Instance.moveStickSizePosition * (SizeRange.y - SizeRange.x),
			SizeRange.x + SettingManager.Instance.moveStickSizePosition * (SizeRange.y - SizeRange.x), transform.localScale.z);
	}
}
