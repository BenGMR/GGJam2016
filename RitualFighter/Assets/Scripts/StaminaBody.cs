using UnityEngine;
using System.Collections;

public class StaminaBody : MonoBehaviour
{

    public GameObject StaminaBar;
    StaminaBarScript staminaBarScript;
    Rigidbody2D rb;
    public Player player;
    public bool isLeftHand = false;
    public bool isRightHand = false;
    HingeJoint2D joint = null;

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

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Player" + (int)player)
        {
            if (joint == null && isLeftHand && Input.GetAxis("LeftTrigger" + ((int)player).ToString()) > .1f)
            {
                joint = this.gameObject.AddComponent<HingeJoint2D>();
                joint.connectedBody = coll.rigidbody;
            }
            if (joint == null && isRightHand && Input.GetAxis("RightTrigger" + ((int)player).ToString()) > .1f)
            {
                joint = this.gameObject.AddComponent<HingeJoint2D>();
                joint.connectedBody = coll.rigidbody;
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
        if (joint != null && isLeftHand && Input.GetAxis("LeftTrigger" + ((int)player).ToString()) <= .1f)
        {
            Destroy(joint);
            joint = null;
        }
        if (joint != null && isRightHand && Input.GetAxis("RightTrigger" + ((int)player).ToString()) <= .1f)
        {
            Destroy(joint);
            joint = null;
        }
    }
}
