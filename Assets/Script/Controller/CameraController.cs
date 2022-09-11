using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoSingleton<CameraController>
{
    public float y;
    public Vector2 xRange;
	public float xPosition
	{
		get
		{
			return (transform.position.x - xRange.x) / (xRange.y - xRange.x);
		}
		set
		{
			transform.position = new Vector3(xRange.x + (float)value *(float)(xRange.y - xRange.x), transform.position.y, transform.position.z);
		}

	}
	public float speed=10f;

    public Camera currentCamera;

	public bool IsFollowingPlayer
	{
		get
		{
			return isFollowingPlayerToggle.isOn;
		}
		set
		{
			isFollowingPlayerToggle.isOn = value;
		}
	}
	public Toggle isFollowingPlayerToggle;
	private void Start()
	{
		currentCamera.transform.position = new Vector3(currentCamera.transform.position.x, y, currentCamera.transform.position.z);
	}
	// Update is called once per frame
	void Update()
    {
		if (currentCamera != null)
		{
			if (!IsFollowingPlayer)
			{
				//if (Input.mousePosition.x < 0 && currentCamera.transform.position.x > xRange.x)
				//{
				//	currentCamera.transform.position = new Vector3(currentCamera.transform.position.x - Time.deltaTime * 5f
				//		, currentCamera.transform.position.y, currentCamera.transform.position.z);
				//}
				//else if (Input.mousePosition.x > Screen.width && currentCamera.transform.position.x < xRange.y)
				//{
				//	currentCamera.transform.position = new Vector3(currentCamera.transform.position.x + Time.deltaTime * 5f
				//		, currentCamera.transform.position.y, currentCamera.transform.position.z);
				//}
			}
			else
			{
				if (PlayerManager.Instance.currentPlayer != null) {
					currentCamera.transform.position = new Vector3(PlayerManager.Instance.currentPlayer.transform.position.x, currentCamera.transform.position.y, currentCamera.transform.position.z);
				}
			}
		}
    }
}
