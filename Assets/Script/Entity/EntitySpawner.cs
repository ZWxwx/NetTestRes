using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class EntitySpawner : MonoBehaviourPunCallbacks
{
	public Team team;
    public Vector2 upLeftPoint;
	public Vector2 downRightPoint;
	GameObject temp;
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(upLeftPoint, downRightPoint);
	}
#endif

	public EntityController spawnEntity(int entityID)
	{
		if (!DataManager.Instance.Entities.ContainsKey(entityID))
		{
			Debug.LogError("无法生成ID为" + entityID.ToString() + "的实体，因为其在DataManager中不存在！");
			return null;
		}
		if (PhotonNetwork.IsMasterClient)
		{
			temp = PhotonNetwork.InstantiateRoomObject("Tenshi1", new Vector2(Random.Range(upLeftPoint.x, downRightPoint.x), Random.Range(upLeftPoint.y, downRightPoint.y)), Quaternion.identity);
			temp.GetComponent<EntityController>().SendInitialData(entityID, (int)team, DataManager.Instance.Entities[entityID].MaxHealth);
			return temp.GetComponent<EntityController>();
		}
		return null;
		
	}

	public EntityController spawnPlayer(int entityID)
	{
		if (!DataManager.Instance.Entities.ContainsKey(entityID))
		{
			Debug.LogError("无法生成ID为" + entityID.ToString() + "的实体，因为其在DataManager中不存在！");
			return null;
		}
		temp = PhotonNetwork.Instantiate("Player", new Vector2(Random.Range(upLeftPoint.x, downRightPoint.x), Random.Range(upLeftPoint.y, downRightPoint.y)), Quaternion.identity);
		temp.GetComponent<EntityController>().SendInitialData(entityID, (int)team, DataManager.Instance.Entities[entityID].MaxHealth);
		return temp.GetComponent<EntityController>();
	}

	void Update()
    {
		#region 测试用生成实体
		if (PhotonNetwork.NickName == "d")
		{
			if (Input.GetKeyDown(KeyCode.Alpha1) && team == Team.Red)
			{
				temp = PhotonNetwork.Instantiate("Tenshi1", new Vector2(Random.Range(upLeftPoint.x, downRightPoint.x), Random.Range(upLeftPoint.y, downRightPoint.y)), Quaternion.identity);
				temp.GetComponent<AIEntityController>().entityInfo.teamId = (int)Team.Red;
				temp.GetComponent<PhotonView>().RPC("ReceiveInitialData", RpcTarget.All, 101);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2) && team == Team.Blue)
			{
				temp = PhotonNetwork.Instantiate("Tenshi1", new Vector2(Random.Range(upLeftPoint.x, downRightPoint.x), Random.Range(upLeftPoint.y, downRightPoint.y)), Quaternion.identity);
				temp.GetComponent<AIEntityController>().entityInfo.teamId = (int)Team.Blue;
				//temp.transform.SetParent(ObjectManager.Instance.battleFieldRoot);
				temp.GetComponent<PhotonView>().RPC("ReceiveInitialData", RpcTarget.All, 101);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3) && team == Team.Red)
			{
				temp = PhotonNetwork.Instantiate("Tenshi1", new Vector2(Random.Range(upLeftPoint.x, downRightPoint.x), Random.Range(upLeftPoint.y, downRightPoint.y)), Quaternion.identity);
				temp.GetComponent<AIEntityController>().entityInfo.teamId = (int)Team.Red;
				//temp.GetComponent<AIEntityController>().entityInfo.entityDataId = 103;
				temp.GetComponent<PhotonView>().RPC("ReceiveInitialData", RpcTarget.All, 103);

			}
			else if (Input.GetKeyDown(KeyCode.Alpha4) && team == Team.Blue)
			{
				temp = PhotonNetwork.Instantiate("Tenshi1", new Vector2(Random.Range(upLeftPoint.x, downRightPoint.x), Random.Range(upLeftPoint.y, downRightPoint.y)), Quaternion.identity);
				temp.GetComponent<AIEntityController>().entityInfo.teamId = (int)Team.Blue;
				//temp.GetComponent<AIEntityController>().entityInfo.entityDataId = 103;
				temp.GetComponent<PhotonView>().RPC("ReceiveInitialData", RpcTarget.All, 103);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha5) && team == Team.Red)
			{

				temp = PhotonNetwork.Instantiate("Tenshi1", new Vector2(Random.Range(upLeftPoint.x, downRightPoint.x), Random.Range(upLeftPoint.y, downRightPoint.y)), Quaternion.identity);
				temp.GetComponent<AIEntityController>().entityInfo.teamId = (int)Team.Red;
				//temp.GetComponent<AIEntityController>().entityInfo.entityDataId = 103;
				temp.GetComponent<PhotonView>().RPC("ReceiveInitialData", RpcTarget.All, 104);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha6) && team == Team.Blue)
			{
				temp = PhotonNetwork.Instantiate("Tenshi1", new Vector2(Random.Range(upLeftPoint.x, downRightPoint.x), Random.Range(upLeftPoint.y, downRightPoint.y)), Quaternion.identity);
				temp.GetComponent<AIEntityController>().entityInfo.teamId = (int)Team.Blue;
				//temp.GetComponent<AIEntityController>().entityInfo.entityDataId = 103;
				temp.GetComponent<PhotonView>().RPC("ReceiveInitialData", RpcTarget.All, 104);
			}
		}
		#endregion
	}
}
