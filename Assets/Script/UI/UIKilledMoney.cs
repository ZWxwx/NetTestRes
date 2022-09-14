using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKilledMoney : MonoBehaviour
{
    public float timeOfStartDisappear;
    public float timeOfEndDisappear;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(onAnime());
    }

    public IEnumerator onAnime()
	{
        yield return new WaitForSeconds(timeOfStartDisappear);
        float i = timeOfStartDisappear;
		while (i < timeOfEndDisappear)
		{
            i += Time.deltaTime;
            Color color = GetComponent<SpriteRenderer>().color;
            Color textColor = GetComponentInChildren<Text>().color;
            GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b,(timeOfEndDisappear-i) / (timeOfEndDisappear - timeOfStartDisappear));
            GetComponentInChildren<Text>().color = new Color(textColor.r, textColor.g, textColor.b,(timeOfEndDisappear-i) / (timeOfEndDisappear - timeOfStartDisappear));
            yield return null;
		}
        Destroy(gameObject);
	}
}
