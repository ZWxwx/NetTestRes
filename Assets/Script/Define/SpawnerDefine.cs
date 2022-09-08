using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn", menuName = "Spawn/NewSpawnDefine")]
public class SpawnerDefine:ScriptableObject
{
	[Header("生成器ID")]
	public int ID;
	[Header("所生成的实体ID")]
	public int entityID;
	[Header("自然生成阈值")]
	public float spawnValue;
}
