using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource sfxSource2;
    public static SoundManager instance = null;
    public AudioClip mainMenu;
    public AudioClip fight;

    public AudioClip boo;
    public AudioClip lightning;
    public AudioClip punch;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }
	void Start () {


	}
	
	void Update () {
	
	}

    public void PlaySound(string soundName)
    {
        if (!sfxSource.isPlaying)
        {
            if (soundName == "Lightning")
            {
                sfxSource.clip = lightning;
            }
            else if (soundName == "Boo")
            {
                sfxSource.clip = boo;
            }
            else if (soundName == "Punch")
            {
                sfxSource.clip = punch;
            }
            else
            {
                return;
            }
            sfxSource.Play();
        }
        else
        {
            if (soundName == "Lightning")
            {
                sfxSource2.clip = lightning;
            }
            else if (soundName == "Boo")
            {
                sfxSource2.clip = boo;
            }
            else if (soundName == "Punch")
            {
                sfxSource2.clip = punch;
            }
            else
            {
                return;
            }
            sfxSource2.Play();
        }
    }

    public void PlaySong(string songName)
    {

        if(songName == "MainMenu")
        {
            musicSource.clip = mainMenu;
        }
        else if(songName == "Fight")
        {
            musicSource.clip = fight;
        }
        else
        {
            return;
        }
        musicSource.Play();
    }
}
