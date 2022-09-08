using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
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
            Debug.LogFormat("����������ƣ�{0}����������ǩ:{1}", mouseHitCollider.gameObject, mouseHitCollider.gameObject.tag);
		}
		else
		{
            Cursor.SetCursor(normalCursor, new Vector2(4f, 4f), CursorMode.Auto);
        }
    }
}
