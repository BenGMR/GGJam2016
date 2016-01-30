using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum Player
{
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4
}

public class RaggedySpineboy : MonoBehaviour {
    public float footForce = 200;
    public float handForce = 200;
    public float torsoForce = 100;
    public float maxYSpeedUp = 7;

    Rigidbody2D torso;
    Rigidbody2D leftFoot;
    Rigidbody2D rightFoot;
    Rigidbody2D leftHand;
    Rigidbody2D rightHand;
    Rigidbody2D hip;

    SkeletonRagdoll2D ragdoll;
    
    public Player player = Player.One;
    public Player player2 = Player.One;

    public bool DisableControls = false;

	void Start () {
		
		ragdoll = GetComponent<SkeletonRagdoll2D>();
        ragdoll.Apply();
        torso = ragdoll.GetRigidbody("Torso");
        leftFoot = ragdoll.GetRigidbody("LeftFoot");
        leftHand = ragdoll.GetRigidbody("LeftHand");
        rightFoot = ragdoll.GetRigidbody("RightFoot");
        rightHand = ragdoll.GetRigidbody("RightHand");
        hip = ragdoll.GetRigidbody("Hip");

        ragdoll.joints["LeftUpperArm"].limits = new JointAngleLimits2D() { min = -90, max = 90 };
        ragdoll.joints["RightUpperArm"].limits = new JointAngleLimits2D() { min = -90, max = 90 };
        ragdoll.joints["LeftLowerArm"].limits = new JointAngleLimits2D() { min = -100, max = 100 };
        ragdoll.joints["RightLowerArm"].limits = new JointAngleLimits2D() { min = -100, max = 100 };
        ragdoll.joints["LeftHand"].limits = new JointAngleLimits2D() { min = -30, max = 30 };
        ragdoll.joints["RightHand"].limits = new JointAngleLimits2D() { min = -30, max = 30 };


        ragdoll.joints["LeftShin"].limits = new JointAngleLimits2D() { min = -10, max = 10 };
        ragdoll.joints["RightShin"].limits = new JointAngleLimits2D() { min = -10, max = 10 };
        ragdoll.joints["LeftFoot"].limits = new JointAngleLimits2D() { min = -10, max = 10 };
        ragdoll.joints["RightFoot"].limits = new JointAngleLimits2D() { min = -10, max = 10 };

    }

    void FixedUpdate()
    {

        if (hip.velocity.y >= maxYSpeedUp)
        {
            hip.velocity = new Vector2(hip.velocity.x, maxYSpeedUp);
        }
    }

    void Update ()
    {
        if (!DisableControls)
        {
            if (Input.GetButton("Start" + ((int)player).ToString()))
            {
                Debug.Log(string.Format("Start pressed on player {0}", player));
                SceneManager.LoadScene("Settings");
            }
            hip.AddForce(new Vector2(0, 500 * Input.GetAxis("RightStickVertical" + ((int)player).ToString())));

            torso.AddForce(new Vector2(500 * Input.GetAxis("RightStickHorizontal" + ((int)player).ToString()), 0));
            

            if (Input.GetButton("A" + ((int)player2).ToString()))
            {
                leftFoot.AddForce((footForce) * new Vector2(Input.GetAxis("LeftStickHorizontal" + ((int)player).ToString()), Input.GetAxis("LeftStickVertical" + ((int)player).ToString())));

                ragdoll.joints["LeftThigh"].limits = new JointAngleLimits2D() { min = -20, max = 100 };
                //ragdoll.leftLegLimit = new JointAngleLimits2D() { min = -60, max = 20 };
            }
            else
            {
                ragdoll.joints["LeftThigh"].limits = new JointAngleLimits2D() { min = 0, max = 0 };
            }
            if (Input.GetButton("B" + ((int)player).ToString()))
            {
                rightFoot.AddForce((footForce) * new Vector2(Input.GetAxis("LeftStickHorizontal" + ((int)player).ToString()), Input.GetAxis("LeftStickVertical" + ((int)player).ToString())));

                ragdoll.joints["RightThigh"].limits = new JointAngleLimits2D() { min = -100, max = 20 };
                //ragdoll.leftLegLimit = new JointAngleLimits2D() { min = -20, max = 60 };
            }
            else
            {
                ragdoll.joints["RightThigh"].limits = new JointAngleLimits2D() { min = 0, max = 0 };
            }
            if (Input.GetButton("X" + ((int)player2).ToString()))
            {
                leftHand.AddForce((handForce) * new Vector2(Input.GetAxis("LeftStickHorizontal" + ((int)player).ToString()), Input.GetAxis("LeftStickVertical" + ((int)player).ToString())));
            }
            if (Input.GetButton("Y" + ((int)player).ToString()))
            {
                rightHand.AddForce((handForce) * new Vector2(Input.GetAxis("LeftStickHorizontal" + ((int)player).ToString()), Input.GetAxis("LeftStickVertical" + ((int)player).ToString())));
            }
        }
    }
}
