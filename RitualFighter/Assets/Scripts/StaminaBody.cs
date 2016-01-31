using UnityEngine;
using System.Collections;

public class StaminaBody : MonoBehaviour
{

    public GameObject StaminaBar;
    StaminaBarScript staminaBarScript;
    Rigidbody2D rb;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();

        }
        if (staminaBarScript == null)
        {
            staminaBarScript = StaminaBar.GetComponent<StaminaBarScript>();
        }
        if (staminaBarScript.Health > 0 && !staminaBarScript.Regening)
        {

            if (coll.gameObject.layer == 31)
            {
                float collisionForce = coll.rigidbody.velocity.magnitude;
                rb.AddForce(coll.relativeVelocity / staminaBarScript.Health);
                StaminaBar.GetComponent<StaminaBarScript>().DecreaseBar(collisionForce / 5f);
            }
            else
            {
                float collisionForce = rb.velocity.magnitude;
                if (collisionForce > 10f)
                {
                    StaminaBar.GetComponent<StaminaBarScript>().DecreaseBar(collisionForce / 5f);

                }
            }
        }
    }

    // Use this for initializationst
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
