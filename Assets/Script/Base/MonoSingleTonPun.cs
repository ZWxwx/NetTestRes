using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MonoSingleTonPun<T> : MonoBehaviourPunCallbacks where T : MonoBehaviourPunCallbacks
{
    public bool global = true;
    [Tooltip("�Ƿ�ֻ��Ϊ���������ĵ���")]
    public bool sceneSingleTon = true;
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType<T>();
            }
            return instance;
        }

    }

    void Start()
    {

        if (global)
        {
            if (!sceneSingleTon)
            {
                DontDestroyOnLoad(this.gameObject);
            }

            if (instance != null && instance != gameObject.GetComponent<T>())
            {
                Destroy(gameObject);
                return;
            }
            instance = gameObject.GetComponent<T>();
        }
        this.OnStart();
    }

    protected virtual void OnStart()
    {

    }
}
