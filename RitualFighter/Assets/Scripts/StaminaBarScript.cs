using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour
{

    public Image FillImage;
    public float Health = 100;
    public bool Regening = false;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {

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

        if (Health < 0)
            if (Health <= 0)
            {
                Regening = true;
                Health = 0;
            }

        FillImage.rectTransform.localScale = new Vector3(Health / 100f, FillImage.rectTransform.localScale.y);
        if (FillImage.rectTransform.localScale.x == 0)
        {
            this.enabled = false;
            FillImage.enabled = false;
            return true;
        }
        else
        {
            this.enabled = true;
        }
        //Returns true if your health has been completed depleted
        if (Health == 0)
        {
            return true;
            FillImage.enabled = true;
        }


        return false;
    }

}
