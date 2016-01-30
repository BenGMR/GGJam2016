using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

    bool loseTimerStart = false;
    float elapsedTime = 0;
    float timeUntilPersonLoses = 3;

    string playerThatLost = "None";

	void Start () {
	
	}
	
	void Update () {

        if (loseTimerStart)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= timeUntilPersonLoses)
            {
                if(playerThatLost == "Player1")
                {
                    Debug.Log("Player 2 Wins!");
                }
                else if(playerThatLost == "Player2")
                {
                    Debug.Log("Player 1 Wins!");
                }
            }
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.transform.parent.tag);
        if (collider.transform.parent.tag == "Player1")
        {
            playerThatLost = "Player1";
            loseTimerStart = true;
            Debug.Log("Ayyyy");
        }
        else if (collider.transform.parent.tag == "Player2")
        {
            playerThatLost = "Player2";
            loseTimerStart = true;
        }
    }
}
