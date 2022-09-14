using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public Canvas mainCanvas;
    public GameObject uiTextPrefab;

	public UIPlayerBattleInfo uiPlayerBattleInfo;

	Collider2D mouseHitCollider;
    public Texture2D normalCursor;
    public Texture2D entityCursor;
    void Update()
    {
        mouseHitCollider = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
        
        if (mouseHitCollider!=null)
		{
			switch (mouseHitCollider.gameObject.tag)
			{
                case "Entity":
                    Cursor.SetCursor(entityCursor, new Vector2(4f, 4f),CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(normalCursor, new Vector2(4f, 4f), CursorMode.Auto);
                    break;
            }
            Debug.LogFormat("检测物体名称：{0}，检测物体标签:{1}", mouseHitCollider.gameObject, mouseHitCollider.gameObject.tag);
		}
		else
		{
            Cursor.SetCursor(normalCursor, new Vector2(4f, 4f), CursorMode.Auto);
        }

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			UIManager.Instance.Show(uiPlayerBattleInfo);
		}
		if (Input.GetKeyUp(KeyCode.Tab))
		{
			UIManager.Instance.Close(uiPlayerBattleInfo);
		}
    }

    public void showText(string content)
	{
        var text = Instantiate(uiTextPrefab, mainCanvas.transform);
        text.GetComponent<UIText>().text.text = content;
	}

    public void closeUI(GameObject uiObj,bool cache=false)
	{
		if (cache)
		{
            uiObj.SetActive(false);
        }
		else
		{
            Destroy(uiObj);
		}
	}

	public void Show(UIResource res)
	{
		if (res.obj != null)
		{
			res.obj.SetActive(true);

		}
		else
		{
			if (res.prefab == null)
			{
				Debug.LogError("UI无预制体!");
			}
			res.obj = Instantiate(res.prefab);
			res.obj.transform.SetParent(UIManager.Instance.mainCanvas.transform);
		}
		
	}

	public void Close(UIResource res)
	{
		if (res.cache)
		{
			res.obj.SetActive(false);
		}
		else
		{
			Destroy(res.obj);
			
		}	
	}
}
