using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEntitySpawner : EntitySpawner,IPunObservable
{
	public List<SpawnerDefine> spawners;
	Dictionary<int,float> spawnerDic=new Dictionary<int, float>();
	GameObject temp;
	//????????
	public float speedRate;
#if UNITY_EDITOR

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(upLeftPoint, downRightPoint);
	}

#endif
	private void Start()
	{
		spawnerDic.Clear();
		for (int i = 0; i < spawners.Count; i++)
		{
			spawnerDic.Add(i, 0);
		}
		StartCoroutine(onSpawn());

	}

	public IEnumerator onSpawn()
	{
		while (true)
		{
			
			yield return new WaitForSeconds(0.1f);
			if (!PhotonNetwork.IsMasterClient)
			{
				continue;
			}
			int r = Random.Range(0, spawners.Count);
			spawnerDic[r] += speedRate;
			if (spawners[r].spawnValue < spawnerDic[r])
			{
				spawnerDic[r] -= spawners[r].spawnValue;
				spawnEntity(spawners[r].entityID);
			}
			
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(spawnerDic);
		}
		else
		{
			spawnerDic = (Dictionary<int,float>)stream.ReceiveNext();
		}
	}
}
