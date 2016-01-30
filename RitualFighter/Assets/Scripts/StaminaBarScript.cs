using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour {

    public Image FillImage;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
    void Update () {

	}

    public bool DecreaseBar(int decreaseAmount)
    {
        float percentModifier = (float)decreaseAmount / 100f;
        float newXValue = FillImage.rectTransform.localScale.x - percentModifier;

        if (newXValue < 0)
        {
            newXValue = 0;
        }

        FillImage.rectTransform.localScale = new Vector3(newXValue, FillImage.rectTransform.localScale.y);
        
        //Returns true if your health has been completed depleted
        if (newXValue == 0)
        {
            return true;
        }

        return false;
    }
}
