using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapBar : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
	public Vector2 realMapSide;
	public Scrollbar mapBar;
	public bool isOnDragBar;
	#region temp
	#endregion

	private void Awake()
	{
		mapBar.onValueChanged.AddListener(setCameraPosition);
	}

	public void setCameraPosition(float value)
	{
		CameraController.Instance.currentCamera.transform.position = new Vector3(realMapSide.x + value * (realMapSide.y - realMapSide.x), CameraController.Instance.currentCamera.transform.position.y, CameraController.Instance.currentCamera.transform.position.z);
	}

	public void refreshPosition()
	{
		Vector3 pos = CameraController.Instance.currentCamera.transform.position;
		mapBar.value = (pos.x - realMapSide.x) / (realMapSide.y - realMapSide.x) > 1 ? 1 : (pos.x - realMapSide.x) / (realMapSide.y - realMapSide.x) < 0 ? 0 : (pos.x - realMapSide.x) / (realMapSide.y - realMapSide.x);
	}

	void Update()
	{
		if (!isOnDragBar)
		{
			refreshPosition();
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		isOnDragBar = true;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		float pointX = Camera.main.ScreenToWorldPoint(eventData.position).x;
		
		setCameraPosition((pointX - realMapSide.x)/(realMapSide.y-realMapSide.x)>1?1:
			(pointX - realMapSide.x) / (realMapSide.y - realMapSide.x)<0?0:
			(pointX - realMapSide.x) / (realMapSide.y - realMapSide.x));
	}

	public void OnDrag(PointerEventData eventData)
	{
		float pointX = Camera.main.ScreenToWorldPoint(eventData.position).x;
		setCameraPosition((pointX - realMapSide.x) / (realMapSide.y - realMapSide.x) > 1 ? 1 :
	(pointX - realMapSide.x) / (realMapSide.y - realMapSide.x) < 0 ? 0 :
	(pointX - realMapSide.x) / (realMapSide.y - realMapSide.x));

		isOnDragBar = false;
	}
}
