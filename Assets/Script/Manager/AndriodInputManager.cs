using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndriodInputManager : MonoSingleton<AndriodInputManager>
{
    public Joystick moveStick;
	public Button attackButton;
	protected override void OnStart()
	{
		base.OnStart();
#if UNITY_ANDROID
		moveStick.gameObject.SetActive(true);
		attackButton.gameObject.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
		moveStick.gameObject.SetActive(false);
		attackButton.gameObject.SetActive(false);
#endif
	}
}
