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

	public static List<string> ToList(string[] strings)
	{
		List<string> list = new List<string>();
		foreach (string st in strings)
		{
			list.Add(st);
		}
		return list;
	}

	public static string[] ToArray(List<string> list)
	{
		string[] strs = new string[20];
		int i = 0;
		foreach (string st in list)
		{
			strs[i] = st;
			i++;
		}
		return strs;
	}
	public void exitGame()
	{
		Application.Quit();
	}
}
