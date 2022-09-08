using System.IO;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _Instance;
    public static DataManager Instance
	{
		get
		{
			if (_Instance == null)
			{
           
                _Instance=FindObjectOfType<DataManager>();
			}
            return _Instance;
		}
	}

    public Dictionary<int, BulletDefine> Bullets = null;
    public Dictionary<int, EntityDefine> Entities = null;
    public List<RuntimeAnimatorController> Animator = null;
    public List<Sprite> BulletSprite = null;
    public List<Sprite> EntityImage = null;
    public DataManager()
    {
        Load();
    }

    public void Load()
    {
        string json = null;

        json = File.ReadAllText(Application.streamingAssetsPath+ "/Data/BulletDefine.txt");
        this.Bullets = JsonConvert.DeserializeObject<Dictionary<int, BulletDefine>>(json);


        json = File.ReadAllText(Application.streamingAssetsPath + "/Data/EntityDefine.txt");
        this.Entities= JsonConvert.DeserializeObject<Dictionary<int, EntityDefine>>(json);
    }

        //public IEnumerator LoadData()
        //{
        //    string json = File.ReadAllText(this.DataPath + "MapDefine.txt");
        //    this.Maps = JsonConvert.DeserializeObject<Dictionary<int, MapDefine>>(json);

        //    yield return null;

        //    json = File.ReadAllText(this.DataPath + "CharacterDefine.txt");
        //    this.Characters = JsonConvert.DeserializeObject<Dictionary<int, CharacterDefine>>(json);

        //    yield return null;

        //    json = File.ReadAllText(this.DataPath + "TeleporterDefine.txt");
        //    this.Teleporters = JsonConvert.DeserializeObject<Dictionary<int, TeleporterDefine>>(json);

        //    yield return null;

        //    json = File.ReadAllText(this.DataPath + "SpawnPointDefine.txt");
        //    this.SpawnPoints = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, SpawnPointDefine>>>(json);

        //    yield return null;
        //}
   
}
