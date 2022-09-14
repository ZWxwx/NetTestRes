using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class UIPlayerBattleInfo : UIWindow
{
    public List<GameObject> pbis=new List<GameObject>();
    public GameObject battleInfoItemPrefab;
    public Transform redSide;
    public Transform blueSide;
    void Update()
    {
        foreach(var go in pbis)
		{
            Destroy(go);
		}
        foreach (var kv in BattleInfoManager.Instance.playerBattleInfos)
		{
            UIPlayerBattleInfoItem pbi;
            pbi = Instantiate(battleInfoItemPrefab, redSide).GetComponent<UIPlayerBattleInfoItem>();
            pbi.playerName.text = kv.Key;
            pbi.refreshInfo();
            pbis.Add(pbi.gameObject);
        }
    }
}
