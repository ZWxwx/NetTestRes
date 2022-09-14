using System.IO;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class DataManager : MonoSingleton<DataManager>
{
	string path = Application.streamingAssetsPath;
    string json = null;



    public Dictionary<int, BulletDefine> Bullets = null;
    public Dictionary<int, EntityDefine> Entities = null;
    public List<RuntimeAnimatorController> Animator = null;
    public List<Sprite> BulletSprite = null;
    public List<Sprite> EntityImage = null;
    public List<Sprite> EntityIdleImage = null;
    protected override void OnStart()
    {
        base.OnStart();
        LoadFileBytes();
    }

    public void Load()
    {


		json = File.ReadAllText(path+ "/Data/BulletDefine.txt");
        this.Bullets = JsonConvert.DeserializeObject<Dictionary<int, BulletDefine>>(json);
        

        json = File.ReadAllText(path + "/Data/EntityDefine.txt");
        this.Entities= JsonConvert.DeserializeObject<Dictionary<int, EntityDefine>>(json);
        
    }


    private void LoadFileBytes()
    {
        string bundlePath;
        bundlePath = Path.Combine(Application.streamingAssetsPath, "Data/EntityDefine.txt");
        //UILoadingMessage.Instance.message.text = "加载资源:" + bundlePath;
        StartCoroutine(GetFileBytes(bundlePath,1));
        bundlePath = Path.Combine(Application.streamingAssetsPath, "Data/BulletDefine.txt");
        //UILoadingMessage.Instance.message.text = "加载资源:" + bundlePath;
        StartCoroutine(GetFileBytes(bundlePath,2));
        //JumpToNextScene();
    }

    IEnumerator GetFileBytes(string path,int id)
    {
        var request = UnityWebRequest.Get(new System.Uri(path));
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            yield break;
        }
        json = request.downloadHandler.text;

        // 根据需求选择返回结果
        switch (id)
		{
            case 1:
                
                this.Entities = JsonConvert.DeserializeObject<Dictionary<int, EntityDefine>>(json);
                break;
            case 2:
                this.Bullets = JsonConvert.DeserializeObject<Dictionary<int, BulletDefine>>(json);
                break;
            default:
				break;
		}

    }

    void JumpToNextScene()
	{
        PhotonNetwork.LoadLevel(2);
	}
}
