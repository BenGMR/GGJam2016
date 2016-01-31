using UnityEngine;
using System.Collections;

public class StaminaBody : MonoBehaviour
{

    public RaggedySpineboy player;
    public bool isLeftHand = false;
    public bool isRightHand = false;
    public bool isHead = false;
    public bool isLeftFoot = false;
    public bool isRightFoot = false;
    HingeJoint2D joint;
    StaminaBarScript stamina;
    Rigidbody2D rb;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (stamina == null)
        {
            stamina = player.StaminaBar.GetComponent<StaminaBarScript>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (stamina.Health > 0 && !stamina.Regening)
        {

            if (coll.gameObject.layer == 31)
            {
                float collisionForce = coll.rigidbody.velocity.magnitude;
                rb.AddForce(coll.relativeVelocity / stamina.Health);
                stamina.DecreaseBar(collisionForce / 5f);
            }
            else
            {
                float collisionForce = rb.velocity.magnitude;
                if (collisionForce > 10f)
                {
                    stamina.DecreaseBar(collisionForce / 5f);

                }
                player.touchingGround = true;
                if (isLeftFoot)
                {
                    player.leftGrounded = true;
                }
                else if (isRightFoot)
                {
                    player.rightGrounded = true;
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Contains("Player"))
        {
            if (joint.enabled = false && isLeftHand && Input.GetAxis("LeftTrigger" + ((int)player.player2).ToString()) > .1f)
            {
                joint = this.gameObject.AddComponent<HingeJoint2D>();
                joint.enabled = true;
                joint.connectedBody = coll.rigidbody;
            }
            if (joint.enabled == false && isRightHand && Input.GetAxis("RightTrigger" + ((int)player.player).ToString()) > .1f)
            {
                joint = this.gameObject.AddComponent<HingeJoint2D>();
                joint.enabled = true;
                joint.connectedBody = coll.rigidbody;
            }
        }
        else
        {
            player.touchingGround = true;
            if (isLeftFoot)
            {
                player.leftGrounded = true;
            }
            else if (isRightFoot)
            {
                player.rightGrounded = true;
            }
        }
    }

    // Use this for initialization
    void OnCollisionExit2D(Collision2D coll)
    {
        if (!coll.gameObject.tag.Contains("Player"))
        {
            player.touchingGround = false;
            if (isLeftFoot)
            {
                player.leftGrounded = false;
            }
            else if (isRightFoot)
            {
                player.rightGrounded = false;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        joint = gameObject.AddComponent<HingeJoint2D>();
        joint.autoConfigureConnectedAnchor = true;
        joint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stamina.Health == 0)
        {
            joint.connectedBody = null;
            joint.enabled = false;
        }
        if (joint.enabled == true && isLeftHand && Input.GetAxis("LeftTrigger" + ((int)player.player2).ToString()) <= .1f)
        {
            Destroy(joint);
            joint = null;
            joint.connectedBody = null;
            joint.enabled = false;
        }
        if (joint.enabled == true && isRightHand && Input.GetAxis("RightTrigger" + ((int)player.player).ToString()) <= .1f)
        {
            Destroy(joint);
            joint = null;
            joint.connectedBody = null;
            joint.enabled = false;
        }
    }
}
