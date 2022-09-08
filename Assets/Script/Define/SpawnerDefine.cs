using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn", menuName = "Spawn/NewSpawnDefine")]
public class SpawnerDefine:ScriptableObject
{
	[Header("������ID")]
	public int ID;
	[Header("�����ɵ�ʵ��ID")]
	public int entityID;
	[Header("��Ȼ������ֵ")]
	public float spawnValue;
}
