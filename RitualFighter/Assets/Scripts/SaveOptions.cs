using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveOptions : MonoBehaviour {
    public Slider music;
    public Slider sound;
    public Toggle help;
    public void Save()
    {
        Debug.Log(music.value.ToString());
        SoundManager.instance.musicSource.volume = music.value;
        SoundManager.instance.sfxSource.volume = sound.value;
        OptionsManager.instance.help = help.isOn;
    }
}
