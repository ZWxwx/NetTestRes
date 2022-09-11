using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool global = true;
    [Tooltip("是否只作为单个场景的单例")]
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