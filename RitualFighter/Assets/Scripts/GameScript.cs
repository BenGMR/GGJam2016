using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public GameObject TopHitBox;
    public GameObject Moon;

    public enum CurrentGameState
    {
        Playing,
        //the short time between someone losing and the game deciding that they lost or won.
        DelayBeforeWin,
        //yay!
        SomeoneWins,

        NobodyWins
    }

    public CurrentGameState currentGameState = CurrentGameState.Playing;
    public ParticleSystem volcanoParticles;
    public Text winText;
    public GameObject Player1;
    public GameObject Player2;
    RaggedySpineboy player1Script;
    RaggedySpineboy player2Script;

    Rigidbody2D winningPlayerRigidBody;
    SkeletonRagdoll2D winningPlayerSkeleton;
    ParticleSystem winningPlayerParticles;

    public bool DEBUG = false;
    
    float elapsedTime = 0;
    float timeUntilPersonLoses = 1;
    float timePerFlash = 0.5f;
    float elapsedMoveToMoonTime = 0;

    public float floatingForce = .1f;

    bool bothPlayersLost = false;

    string playerThatLost = "None";

    void Start()
    {
        winText.gameObject.SetActive(false);
        player1Script = Player1.GetComponent<RaggedySpineboy>();
        player2Script = Player2.GetComponent<RaggedySpineboy>();

    }

    void Update()
    {
        if (currentGameState == CurrentGameState.Playing)
        {

        }
        else if (currentGameState == CurrentGameState.DelayBeforeWin)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeUntilPersonLoses)
            {
                volcanoParticles.Play();
                currentGameState = CurrentGameState.SomeoneWins;

                if (bothPlayersLost)
                {
                    winText.text = "Nobody Wins...";
                }
                else if (playerThatLost == "Player1")
                {
                    winText.text = "Player 2 Wins!";
                    winningPlayerSkeleton = Player2.GetComponent<SkeletonRagdoll2D>();
                    winningPlayerRigidBody = winningPlayerSkeleton.GetRigidbody("Torso");
                    winningPlayerParticles = Player2.GetComponentInChildren<ParticleSystem>();
                    winningPlayerParticles.Play();
                }
                else if (playerThatLost == "Player2")
                {
                    winText.text = "Player 1 Wins!";
                    winningPlayerSkeleton = Player1.GetComponent<SkeletonRagdoll2D>();
                    winningPlayerRigidBody = winningPlayerSkeleton.GetRigidbody("Torso");
                    winningPlayerParticles = Player1.GetComponentInChildren<ParticleSystem>();
                    winningPlayerParticles.Play();
                }
                elapsedTime = 0;
            }
        }
        else if (currentGameState == CurrentGameState.SomeoneWins)
        {
            elapsedMoveToMoonTime += Time.deltaTime;
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timePerFlash)
            {
                elapsedTime = 0;
                winText.gameObject.SetActive(!winText.gameObject.activeSelf);
            }

            if (!bothPlayersLost)
            {
                winningPlayerParticles.transform.position = winningPlayerRigidBody.transform.position;
                winningPlayerRigidBody.transform.position = Vector2.MoveTowards(new Vector2(winningPlayerRigidBody.transform.position.x,winningPlayerRigidBody.transform.position.y), Moon.transform.position, elapsedMoveToMoonTime/6);
                winningPlayerRigidBody.velocity = Vector2.zero;
                if (DEBUG)
                {
                    Debug.Log("Winning player is being lifted");
                }

            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.parent.tag == "Player1")
        {
            if (playerThatLost == "Player2")
            {
                bothPlayersLost = true;
            }

            playerThatLost = "Player1";
            currentGameState = CurrentGameState.DelayBeforeWin;

            player1Script.GameOver = true;
            player2Script.GameOver = true;
        }
        else if (collider.transform.parent.tag == "Player2")
        {
            if (playerThatLost == "Player1")
            {
                bothPlayersLost = true;
            }
            playerThatLost = "Player2";
            currentGameState = CurrentGameState.DelayBeforeWin;

            player1Script.GameOver = true;
            player2Script.GameOver = true;
        }
    }
}
