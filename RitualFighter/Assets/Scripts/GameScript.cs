using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    public enum CurrentGameState
    {
        Playing,
        //the short time between someone losing and the game deciding that they lost or won.
        DelayBeforeWin,
        //yay!
        SomeoneWins
    }

    public CurrentGameState currentGameState = CurrentGameState.Playing;
    public ParticleSystem volcanoParticles;
    public Text winText;

    bool loseTimerStart = false;
    float elapsedTime = 0;
    float timeUntilPersonLoses = 1;
    float timePerFlash = 0.5f;

    string playerThatLost = "None";

	void Start () {
        winText.gameObject.SetActive(false);
	}
	
	void Update () {
        if(currentGameState == CurrentGameState.Playing)
        {

        }
        else if (currentGameState == CurrentGameState.DelayBeforeWin)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= timeUntilPersonLoses)
            {
                volcanoParticles.Play();
                currentGameState = CurrentGameState.SomeoneWins;
                if (playerThatLost == "Player1")
                {
                    winText.text = "Player 2 Wins!";
                }
                else if(playerThatLost == "Player2")
                {
                    winText.text = "Player 1 Wins!";
                }
                elapsedTime = 0;
            }
        }
        else if(currentGameState == CurrentGameState.SomeoneWins)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= timePerFlash)
            {
                elapsedTime = 0;
                winText.gameObject.SetActive(!winText.gameObject.activeSelf);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.parent.tag == "Player1")
        {
            playerThatLost = "Player1";
            currentGameState = CurrentGameState.DelayBeforeWin;
        }
        else if (collider.transform.parent.tag == "Player2")
        {
            playerThatLost = "Player2";
            currentGameState = CurrentGameState.DelayBeforeWin;
        }
    }
}
