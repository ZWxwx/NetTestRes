using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIEntityController))]
public class TowerEntity : MonoBehaviour
{
    public Team team;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AIEntityController>().entityInfo.entityDataId = 105;
        GetComponent<AIEntityController>().entityInfo.teamId = (int)team;
        GetComponent<AIEntityController>().RefreshEntity();
        //GetComponent<Rigidbody2D>().simulated = false;
    }
}
