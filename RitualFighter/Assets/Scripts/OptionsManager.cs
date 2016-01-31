using UnityEngine;
using System.Collections;

public class OptionsManager : MonoBehaviour {

    public static OptionsManager instance;
    public float soundVolume = 1;
    public float musicVolume = 1;
    public bool help = true;


    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
