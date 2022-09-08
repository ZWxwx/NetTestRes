using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDefine
{
	public int ID { get; set; }
	public string Name { get; set; }
	public string Type { get; set; }
	public float Speed { get; set; }
	public float MaxHealth { get; set; }

	public float Attack { get; set; }

	public float AttackRange { get; set; }

	public float ViewRange { get; set; }

	public int AttackPosition { get; set; }
	public float AttackDuring { get; set; }

	
	public int BulletDataId { get; set; }
	public int DefeatedAnimId { get; set; }

	public int Price{ get; set; }

	public int ImageID { get; set; }
}
