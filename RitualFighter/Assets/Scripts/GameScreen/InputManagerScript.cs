using UnityEngine;
using System.Collections;

public class InputManagerScript : MonoBehaviour {

    public GameObject p1StaminaBar;
    public GameObject p2StaminaBar;

    StaminaBarScript p1Bar;
    StaminaBarScript p2Bar;

	void Start () {
        p1Bar = p1StaminaBar.GetComponent<StaminaBarScript>();
        p2Bar = p2StaminaBar.GetComponent<StaminaBarScript>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            p1Bar.DecreaseBar(1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            p2Bar.DecreaseBar(1);
        }
	}
}
