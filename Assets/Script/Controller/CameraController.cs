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
	private void Start()
	{
		currentCamera.transform.position = new Vector3(currentCamera.transform.position.x, y, currentCamera.transform.position.z);
	}
	// Update is called once per frame
	void Update()
    {
		if (currentCamera != null)
		{
			if (!SettingManager.Instance.IsCameraFollowing)
			{

			}
			else
			{
				if (PlayerManager.Instance.currentPlayer != null) {
					currentCamera.transform.position = new Vector3(PlayerManager.Instance.currentPlayer.transform.position.x, currentCamera.transform.position.y, currentCamera.transform.position.z);
				}
			}
			if (currentCamera.transform.position.x < xRange.x)
			{
				currentCamera.transform.position = new Vector3(xRange.x, currentCamera.transform.position.y,
					currentCamera.transform.position.z);
			}
			else if (currentCamera.transform.position.y > xRange.y)
			{
				currentCamera.transform.position = new Vector3(xRange.y, currentCamera.transform.position.y,
					currentCamera.transform.position.z);
			}
		}
    }
}
