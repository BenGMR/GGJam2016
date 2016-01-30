using UnityEngine;
using System.Collections;

public class RaggedySpineboy : MonoBehaviour {

	public LayerMask groundMask;
	public float restoreDuration = 0.5f;
	public Vector2 launchVelocity = new Vector2(0,10);

	SkeletonRagdoll2D ragdoll;

	void Start () {
		
		ragdoll = GetComponent<SkeletonRagdoll2D>();
        ragdoll.Apply();
        ragdoll.GetRigidbody("right hand").velocity = new Vector2(-10, 100);
        
    }

	void Update () {
        ragdoll.RootRigidbody.AddForce(new Vector2(500 * Input.GetAxis("Horizontal"), 0));
        ragdoll.GetRigidbody("head").AddForce(new Vector2(0, 500 * Input.GetAxis("Vertical")));
	}
    

	void Launch () {
		ragdoll.RootRigidbody.velocity = new Vector2(Random.Range(-launchVelocity.x, launchVelocity.x), launchVelocity.y);
	}

	IEnumerator Restore () {
		Vector3 estimatedPos = ragdoll.EstimatedSkeletonPosition;
		Vector3 rbPosition = ragdoll.RootRigidbody.position;

		Vector3 skeletonPoint = estimatedPos;
		RaycastHit2D hit = Physics2D.Raycast((Vector2)rbPosition, (Vector2)(estimatedPos - rbPosition), Vector3.Distance(estimatedPos, rbPosition), groundMask);
		if (hit.collider != null)
			skeletonPoint = hit.point;
		

		ragdoll.RootRigidbody.isKinematic = true;
		ragdoll.SetSkeletonPosition(skeletonPoint);

		yield return ragdoll.SmoothMix(0, restoreDuration);
		ragdoll.Remove();
	}

	IEnumerator WaitUntilStopped () {
		yield return new WaitForSeconds(0.5f);

		float t = 0;
		while (t < 0.5f) {
			if (ragdoll.RootRigidbody.velocity.magnitude > 0.09f)
				t = 0;
			else
				t += Time.deltaTime;

			yield return null;
		}

		StartCoroutine(Restore());
	}
}
