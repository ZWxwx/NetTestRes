using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MapBar : MonoBehaviour
{
    public Rect mapRect;
    public Vector2 offset;
    public RectTransform currentView;
    public float CurrentViewPosition
	{
		get
		{
            return (currentView.transform.position.x - mapRect.min.x) / (mapRect.max.x - mapRect.min.x);
        }
		set
		{
            currentView.transform.position = new Vector3(mapRect.min.x + value * (mapRect.max.x - mapRect.min.x), transform.position.y, transform.position.z);
			CameraController.Instance.xPosition = CurrentViewPosition;
        }
	}

    public bool onSet;

	#region temp



	#endregion

	private void Awake()
	{
		mapRect = new Rect((Vector2)transform.position+new Vector2(this.GetComponent<RectTransform>().rect.min.x, this.GetComponent<RectTransform>().rect.min.y),
			new Vector2(this.GetComponent<RectTransform>().rect.width, this.GetComponent<RectTransform>().rect.height));
		Debug.Log(mapRect);
		//Debug.Log((Vector2)this.GetComponent<RectTransform>().position);
		//Debug.Log(this.GetComponent<RectTransform>().rect.width);
		//Debug.Log(this.GetComponent<RectTransform>().pivot);
	}

	// Update is called once per frame
	void Update()
    {

		Rect tRect = new Rect(offset, mapRect.size);
		if (Input.GetKeyDown(KeyCode.Mouse0)&& mapRect.Contains(Input.mousePosition))
		{
            Debug.Log("ÉèÖÃµØÍ¼Î»ÖÃ");
            onSet = true;
		}
		if (onSet)
		{
            CurrentViewPosition = Input.mousePosition.x > mapRect.max.x ? 1 : Input.mousePosition.x < mapRect.min.x ? 0 :
                                 (Input.mousePosition.x - mapRect.min.x) / (mapRect.width); 

        }
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
            onSet = false;
		}

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Debug.Log(Input.mousePosition);
			Debug.Log(mapRect.min.x + "," + mapRect.max.x + "," + mapRect.min.y + "," + mapRect.max.y);
			Debug.Log(offset);
			Debug.Log(mapRect);

		}
	}
}
