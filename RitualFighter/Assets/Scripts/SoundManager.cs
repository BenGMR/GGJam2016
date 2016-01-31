using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public static SoundManager instance = null;
	void Start () {

        instance = this;

	}
	
	void Update () {
	
	}

    public void PlaySingle(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }
}
