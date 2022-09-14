using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRetinueInfoItem : MonoBehaviour
{
    public int entityId;
    public Text Name;
    public Image entityImage;
    public Text Speed;
    public Text ATK;
    public Text ATKRange;
    public Text ViewRange;
    public Text ATKDuring;
    public Text PPrice;
	public Text Type;

    public void refreshInfoByEntityID()
	{
		
        EntityDefine ed = DataManager.Instance.Entities[entityId];
        if (ed == null)
        {
            Debug.LogError("entityID错误，无法加载信息");
            return;
        }
        Name.text = ed.Name;
        entityImage.overrideSprite = DataManager.Instance.EntityIdleImage[ed.DefeatedAnimId];
        //entityImage.SetNativeSize();
        Speed.text = ed.Speed.ToString();
        ATK.text = ed.Attack.ToString();
        ATKRange.text = ed.AttackRange.ToString();
        ViewRange.text = ed.ViewRange.ToString();
        ATKDuring.text = ed.AttackDuring.ToString();
        PPrice.text = ed.Price.ToString();
        Type.text = ed.Type;

	}
	// Start is called before the first frame update
	void Start()
    {
        refreshInfoByEntityID();
    }
}
