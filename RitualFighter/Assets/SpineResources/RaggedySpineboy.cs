using UnityEngine;
using System.Collections;

public enum Player
{
    None = 0,
    One = 1,
    Two = 2
}

public class RaggedySpineboy : MonoBehaviour {

	public LayerMask groundMask;
	public float restoreDuration = 0.5f;
	public Vector2 launchVelocity = new Vector2(0,10);

	SkeletonRagdoll2D ragdoll;
    public Player player = Player.None;

	void Start () {
		
		ragdoll = GetComponent<SkeletonRagdoll2D>();
        ragdoll.Apply();
        ragdoll.GetRigidbody("right hand").velocity = new Vector2(-10, 100);
        
    }

	void Update () {
        ragdoll.RootRigidbody.AddForce(new Vector2(500 * Input.GetAxis("Horizontal" + ((int)player).ToString()), 0));
        ragdoll.GetRigidbody("head").AddForce(new Vector2(0, 500 * Input.GetAxis("Vertical" + ((int)player).ToString())));
	}
}
