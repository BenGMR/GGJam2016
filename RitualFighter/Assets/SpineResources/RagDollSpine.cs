using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Player
{
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4
}

public class RagDollSpine : MonoBehaviour {
    public float footForce = 200;
    public float handForce = 200;
    public float torsoForce = 100;
    public float maxYSpeedUp = 7;
    public int knockOutAmount = 1;
    public static bool MenuUp = false;
    public Sprite ATexture;
    public Sprite BTexture;
    public Sprite XTexture;
    public Sprite YTexture;
    public Sprite P1Texture;
    public Sprite P2Texture;
    public Sprite P3Texture;
    public Sprite P4Texture;

    GameObject a;
    GameObject b;
    GameObject x;
    GameObject y;
    GameObject p1;
    GameObject p2;
    GameObject p3;
    GameObject p4;




    Rigidbody2D torso;
    Rigidbody2D leftFoot;
    Rigidbody2D rightFoot;
    Rigidbody2D leftHand;
    Rigidbody2D rightHand;
    Rigidbody2D hip;

    SkeletonRagdoll2D ragdoll;
    
    public Player player = Player.One;
    public Player player2 = Player.One;

    public GameObject StaminaBar;
    StaminaBarScript staminaBarScript;
    public bool GameOver = false;
    public bool touchingGround = false;

    bool DisableControls = false;
    public bool leftGrounded = false;
    public bool rightGrounded = false;
    public bool leftGrabbing = false;
    public bool rightGrabbing = false;

    public Color color = Color.white;

	void Start ()
    {
        a = new GameObject();
        SpriteRenderer Arender = a.AddComponent<SpriteRenderer>();
        Arender.sprite = ATexture;
        Arender.sortingOrder = 100;
        a.transform.parent = transform;

        b = new GameObject();
        SpriteRenderer Brender = b.AddComponent<SpriteRenderer>();
        Brender.sprite = BTexture;
        Brender.sortingOrder = 100;
        b.transform.parent = transform;

        x = new GameObject();
        SpriteRenderer Xrender = x.AddComponent<SpriteRenderer>();
        Xrender.sprite = XTexture;
        Xrender.sortingOrder = 100;
        x.transform.parent = transform;

        y = new GameObject();
        SpriteRenderer Yrender = y.AddComponent<SpriteRenderer>();
        Yrender.sprite = YTexture;
        Yrender.sortingOrder = 100;
        y.transform.parent = transform;

        staminaBarScript = StaminaBar.GetComponent<StaminaBarScript>();
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

    IEnumerator staminaRegen()
    {
        while (staminaBarScript.Health < 100)
        {
            yield return new WaitForSeconds(.01f * knockOutAmount);
            staminaBarScript.Health++;
        }
        staminaBarScript.Health = 100;
        DisableControls = false;
        staminaBarScript.Regening = false;
        knockOutAmount++;
    }

    void Update ()
    {
        if(OptionsManager.instance.help)
        {
            a.transform.localPosition = leftFoot.gameObject.transform.localPosition;
            b.transform.localPosition = rightFoot.gameObject.transform.localPosition;
            x.transform.localPosition = leftHand.gameObject.transform.localPosition;
            y.transform.localPosition = rightHand.gameObject.transform.localPosition;
        }
        //ragdoll.skeleton.FindSlot("FighterClothes").SetColor(color);
        if(staminaBarScript.Health == 0 && !DisableControls)
        {
            DisableControls = true;
            StartCoroutine(staminaRegen());
        }
        if(MenuUp && CanvasManager.scenes.Peek().name == "Game")
        {
            MenuUp = false;
        }
        if (!DisableControls && !GameOver && !MenuUp)
        {
            if (leftGrabbing)
            {
                staminaBarScript.Health -= .15f;
            }
            if (rightGrabbing)
            {
                staminaBarScript.Health -= .15f;
            }
            staminaBarScript.Health = Mathf.Clamp(staminaBarScript.Health + .1f, 0, 100);
            if (Input.GetButton("Start" + ((int)player).ToString()) || Input.GetButton("Start" + ((int)player2).ToString()))
            {
                MenuUp = true;
                SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
                Scene old;
                if (CanvasManager.scenes.Count < 1)
                {
                    old = SceneManager.GetActiveScene();
                    CanvasManager.scenes.Push(old);
                }
                old = CanvasManager.scenes.Peek();
                CanvasManager.scenes.Push(SceneManager.GetSceneByName("Settings"));
                //CanvasManager.Canvases[old].enabled = false;
                CanvasManager.EventSystems[old].gameObject.SetActive(false);
                CanvasManager.Canvases[old].gameObject.SetActive(false);
            }
            if (leftGrounded && rightGrounded && Input.GetButton("LeftBumper" + ((int)player2).ToString()))
            {
                torso.AddForce(new Vector2(0, 250), ForceMode2D.Impulse);
            }
            if (leftGrounded && rightGrounded && Input.GetButton("RightBumper" + ((int)player).ToString()))
            {
                torso.AddForce(new Vector2(0, 250), ForceMode2D.Impulse);
            }

            hip.AddForce(new Vector2(0, 500 * Input.GetAxis("RightStickVertical" + ((int)player).ToString())));

            torso.AddForce(new Vector2(500 * Input.GetAxis("RightStickHorizontal" + ((int)player).ToString()), 0));
            

            if (Input.GetButton("A" + ((int)player2).ToString()))
            {
                leftFoot.AddForce((footForce) * (touchingGround ? 1 : .5f) * new Vector2(Input.GetAxis("LeftStickHorizontal" + ((int)player2).ToString()), Input.GetAxis("LeftStickVertical" + ((int)player2).ToString())));

                if (touchingGround)
                {
                    ragdoll.joints["LeftThigh"].limits = new JointAngleLimits2D() { min = -20, max = 100 };
                }
                else
                {
                    ragdoll.joints["LeftThigh"].limits = new JointAngleLimits2D() { min = -20, max = 20 };
                }
            }
            else
            {
                ragdoll.joints["LeftThigh"].limits = new JointAngleLimits2D() { min = 0, max = 0 };
            }
            if (Input.GetButton("B" + ((int)player).ToString()))
            {
                rightFoot.AddForce((footForce) * (touchingGround ? 1 : .5f) * new Vector2(Input.GetAxis("LeftStickHorizontal" + ((int)player).ToString()), Input.GetAxis("LeftStickVertical" + ((int)player).ToString())));

                if (touchingGround)
                {
                    ragdoll.joints["RightThigh"].limits = new JointAngleLimits2D() { min = -100, max = 20 };
                }
                else
                {
                    ragdoll.joints["RightThigh"].limits = new JointAngleLimits2D() { min = -20, max = 20 };
                }
            }
            else
            {
                ragdoll.joints["RightThigh"].limits = new JointAngleLimits2D() { min = 0, max = 0 };
            }
            if (Input.GetButton("X" + ((int)player2).ToString()))
            {
                leftHand.AddForce((handForce * (leftGrabbing ? 2 : 1)) * new Vector2(Input.GetAxis("LeftStickHorizontal" + ((int)player2).ToString()), Input.GetAxis("LeftStickVertical" + ((int)player2).ToString())));
            }
            if (Input.GetButton("Y" + ((int)player).ToString()))
            {
                rightHand.AddForce((handForce * (rightGrabbing ? 2 : 1)) * new Vector2(Input.GetAxis("LeftStickHorizontal" + ((int)player).ToString()), Input.GetAxis("LeftStickVertical" + ((int)player).ToString())));
            }
        }
    }


}
