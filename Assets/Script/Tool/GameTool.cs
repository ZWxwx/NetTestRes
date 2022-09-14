using System.Collections;
using System.Collections.Generic;

using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

public class GameTool : MonoBehaviour
{
	public static Vector2 SerPosition(int value)
	{
		if (value % 200 > 100)
		{
			return new Vector2((value / 200 + 1)*0.01f, (value - (value / 200 + 1) * 200)*0.01f);
		}
		else
		{
			return new Vector2((value / 200)*0.01f, (value - value / 200 * 200)*0.01f);
		}
	}

#if UNITY_EDITOR

	[MenuItem("MenuItem/ToBattleScene")]
	public static void JumpToBattleScene()
	{
		EditorSceneManager.OpenScene("Assets/Scenes/Battle.unity");
	}
	[MenuItem("MenuItem/ToLoginScene")]
	public static void JumpToLoginScene()
	{
		EditorSceneManager.OpenScene("Assets/Scenes/Login.unity");
	}
#endif

}
