using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIEntityController))]
public class TowerEntity : MonoBehaviour
{
    Vector3 originSize;
    public Team team;
    public int entityDataId;
    // Start is called before the first frame update
    void Start()
    {
        originSize = transform.localScale;
        GetComponent<AIEntityController>().entityInfo.entityDataId = entityDataId;
        GetComponent<AIEntityController>().entityInfo.teamId = (int)team;
        GetComponent<AIEntityController>().RefreshEntity();
        //GetComponent<Rigidbody2D>().simulated = false;
    }

	private void Update()
	{
        transform.localScale = originSize;
    }
}
