using UnityEngine;
using System.Collections;

public class SoundManagerSettings : MonoBehaviour {

    public void PlayButtonSound()
    {
        SoundManager.instance.PlaySound("Click");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
