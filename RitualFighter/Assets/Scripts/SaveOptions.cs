using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveOptions : MonoBehaviour {
    public Slider music;
    public Slider sound;
    public Toggle help;
    public void Save()
    {
        OptionsManager.instance.musicVolume = music.value;
        OptionsManager.instance.soundVolume = sound.value;
        OptionsManager.instance.help = help.isOn;
    }
}
