using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> BGMS = new List<AudioClip>();
    public Toggle bgmToggle;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        bgmToggle.onValueChanged.AddListener(bgmOpen);
    }

    void bgmOpen(bool value)
	{
		if (value)
		{
            audioSource.clip = BGMS[Random.Range(0, BGMS.Count - 1)];
            audioSource.Play();
        }
		else
		{
            audioSource.Pause();

        }
    }

    // Update is called once per frame
    void Update()
    {
            
		if (bgmToggle.isOn && !audioSource.isPlaying)
        {
            audioSource.clip = BGMS[Random.Range(0, BGMS.Count - 1)];
            audioSource.Play();
        }
    }
}
