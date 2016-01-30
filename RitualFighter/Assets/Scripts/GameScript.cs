using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{

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

    float elapsedTime = 0;
    float timeUntilPersonLoses = 1;
    float timePerFlash = 0.5f;

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
                }
                else if (playerThatLost == "Player2")
                {
                    winText.text = "Player 1 Wins!";
                }
                elapsedTime = 0;
            }
        }
        else if (currentGameState == CurrentGameState.SomeoneWins)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timePerFlash)
            {
                elapsedTime = 0;
                winText.gameObject.SetActive(!winText.gameObject.activeSelf);
            }
        }
        else if (currentGameState == CurrentGameState.NobodyWins)
        {
            volcanoParticles.Play();
            winText.gameObject.SetActive(true);
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

            player1Script.DisableControls = true;
            player2Script.DisableControls = true;
        }
        else if (collider.transform.parent.tag == "Player2")
        {
            if (playerThatLost == "Player1")
            {
                bothPlayersLost = true;
            }
            playerThatLost = "Player2";

                currentGameState = CurrentGameState.DelayBeforeWin;

            player1Script.DisableControls = true;
            player2Script.DisableControls = true;
        }
    }
}
