using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITeamButton : UISelectedItem
{
    public Team team;
    public GameObject edge;
	public override void Select(bool f)
	{
		if (f)
		{
            GameManager.Instance.selectedTeam = team;
            edge.SetActive(true);
			foreach (var item in GameManager.Instance.teamButtons)
			{
				if (!item.Equals(this))
				{
					item.Selected = false;
				}
			}
		}
		else
		{
			edge.SetActive(false);
		}

	}
}
