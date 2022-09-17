using UnityEngine;
using UnityEngine.UI;

public class UIEntityInfo : MonoBehaviour
{
	public EntityController entity;
    public Text nameText;

	private void Start()
	{
		refreshInfo();
	}

	private void Update()
	{
		if (entity) {
			this.transform.localScale = new Vector3(entity.transform.localScale.x,transform.localScale.y, transform.localScale.z);
		}
	}

	public void refreshInfo()
	{
		if (entity&&entity.entityInfo.isAI)
		{
			nameText.text = DataManager.Instance.Entities[entity.entityInfo.entityDataId].Name;
		}
	}
}
