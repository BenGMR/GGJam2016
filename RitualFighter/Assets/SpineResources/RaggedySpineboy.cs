using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum Player
{
    None = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4
}

public class RaggedySpineboy : MonoBehaviour {

	public LayerMask groundMask;
	public float restoreDuration = 0.5f;
	public Vector2 launchVelocity = new Vector2(0,10);

    public float footForce = 200;
    public float handForce = 200;
    public float torsoForce = 100;
    public float maxYSpeedUp = 7;

    int powerDivisor = 1;

    Rigidbody2D torso;
    Rigidbody2D leftFoot;
    Rigidbody2D rightFoot;
    Rigidbody2D leftHand;
    Rigidbody2D rightHand;

	SkeletonRagdoll2D ragdoll;
    public Player player = Player.None;
    public Player player2 = Player.None;

	void Start () {
		
		ragdoll = GetComponent<SkeletonRagdoll2D>();
        ragdoll.Apply();

        torso = ragdoll.GetRigidbody("torso");
        leftFoot = ragdoll.GetRigidbody("left foot");
        leftHand = ragdoll.GetRigidbody("left hand");
        rightFoot = ragdoll.GetRigidbody("right foot");
        rightHand = ragdoll.GetRigidbody("right hand");
    }

	void Update () {
        powerDivisor = 1;

        if (Input.GetButton("A" + ((int)player).ToString()))
        {
            powerDivisor++;
        }
        if (Input.GetButton("X" + ((int)player).ToString()))
        {
            powerDivisor++;
        }
        if (Input.GetButton("B" + ((int)player).ToString()))
        {
            powerDivisor++;
        }
        if (Input.GetButton("Y" + ((int)player).ToString()))
        {
            powerDivisor++;
        }
        powerDivisor = 1;

        if (Input.GetButton("Start" + ((int)player).ToString()))
        {
            Debug.Log(string.Format("Start pressed on player {0}", player));
            SceneManager.LoadScene(3);
        }

        ragdoll.RootRigidbody.AddForce(new Vector2(500 * Input.GetAxis("RightStickHorizontal" + ((int)player).ToString()), 0));

        if(Input.GetButtonDown("RightBumper" + ((int)player).ToString()))
        {
            torso.AddForce(new Vector2(0, torsoForce), ForceMode2D.Impulse);

        }
        if (torso.velocity.y >= maxYSpeedUp)
        {
            torso.velocity = new Vector2(ragdoll.GetRigidbody("torso").velocity.x, maxYSpeedUp);
        }


        if (Input.GetButton("A" + ((int)player+1).ToString()))
        {
            leftFoot.AddForce((footForce/powerDivisor) * new Vector2(Input.GetAxis("LeftStickHorizontal" + ((int)player).ToString()), Input.GetAxis("LeftStickVertical" + ((int)player).ToString())));
        }
        if (Input.GetButton("B" + ((int)player).ToString()))
        {
            rightFoot.AddForce((footForce / powerDivisor) * new Vector2(Input.GetAxis("LeftStickHorizontal" + ((int)player).ToString()), Input.GetAxis("LeftStickVertical" + ((int)player).ToString())));
        }
        if (Input.GetButton("X" + ((int)player+1).ToString()))
        {
            leftHand.AddForce((handForce / powerDivisor) * new Vector2(Input.GetAxis("LeftStickHorizontal" + ((int)player).ToString()), Input.GetAxis("LeftStickVertical" + ((int)player).ToString())));
        }
        if (Input.GetButton("Y" + ((int)player).ToString()))
        {
            rightHand.AddForce((handForce / powerDivisor) * new Vector2(Input.GetAxis("LeftStickHorizontal" + ((int)player).ToString()), Input.GetAxis("LeftStickVertical" + ((int)player).ToString())));
        }
    }
}
