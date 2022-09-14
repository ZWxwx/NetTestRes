using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GameTool.SerPosition(8040));
        Debug.Log(GameTool.SerPosition(6570));
        Debug.Log(GameTool.SerPosition(-8040));

        Debug.Log(GameTool.SerPosition(-6570));
    }


}
