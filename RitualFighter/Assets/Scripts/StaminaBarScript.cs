using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour {

    public Image FillImage;
    public float Health = 100;
    public bool Regening = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
    void Update () {

        FillImage.rectTransform.localScale = new Vector3(Health / 100f, FillImage.rectTransform.localScale.y, 1);
        if (Health <= 0)
        {
            Regening = true;
            Health = 0;
            FillImage.enabled = false;
        }
        else
        {
            FillImage.enabled = true;
        }
    }

    public bool DecreaseBar(float decreaseAmount)
    {
        Health -= decreaseAmount;

        if (Health <= 0)
        {
            Regening = true;
            Health = 0;
            FillImage.enabled = false;
            return true;
        }
        else
        {
            FillImage.enabled = true;
        }


        return false;
    }
}
