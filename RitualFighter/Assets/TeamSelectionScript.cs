using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class TeamSelectionScript : MonoBehaviour
{
    public static TeamSelectionScript instance;

    public enum Team
    {
        left,
        right,
        none
    }

    public Controller p1, p2, p3, p4;
    public Controller[] players = new Controller[4];
    public List<Controller> leftTeam = new List<Controller>();
    public List<Controller> rightTeam = new List<Controller>();
    public bool Startable = false;
    public Button playButton;
    public Image WarningMessage;
    bool showMessage = false;
    float messageAlpha = 0f;

    // Use this for initialization
    void Start()
    {
        players[0] = p1;
        players[1] = p2;
        players[2] = p3;
        players[3] = p4;
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].Ready)
            {
                if (players[i].team == Team.left && !leftTeam.Contains(players[i]))
                {
                    if (leftTeam.Count < 2)
                    {
                        leftTeam.Add(players[i]);
                    }
                }
                if (players[i].team == Team.right && !rightTeam.Contains(players[i]))
                {
                    if (leftTeam.Count < 2)
                    {
                        rightTeam.Add(players[i]);
                    }
                }
            }
            else
            {
                if (leftTeam.Contains(players[i]))
                {
                    leftTeam.Remove(players[i]);
                }
                else if (rightTeam.Contains(players[i]))
                {
                    rightTeam.Remove(players[i]);
                }
            }
        }
        if (leftTeam.Count >= 1 && rightTeam.Count >= 1 && leftTeam.Count < 3 && rightTeam.Count < 3)
        {
            Startable = true;
            playButton.enabled = true;
        }
        else if (leftTeam.Count >= 3 || rightTeam.Count >= 3)
        {
            showMessage = true;
        }
        else
        {
            Startable = false;
            playButton.enabled = false;
        }
        if (showMessage && messageAlpha == 0f)
        {
            WarningMessage.gameObject.SetActive(true);
            messageAlpha = 1f;
        }

        if (messageAlpha > 0f)
        {
            messageAlpha -= .02f;
            WarningMessage.color = new Color(1, 1, 1, messageAlpha);
        }
        if(messageAlpha <= .05f)
        {
            messageAlpha -= messageAlpha;
            WarningMessage.color = new Color(1, 1, 1, messageAlpha);
            WarningMessage.gameObject.SetActive(false);
            showMessage = false;
        }
        for (int i = 1; i < players.Length + 1; i++)
        {
            if (Startable)
            {
                if (Input.GetButton("Start" + i.ToString()))
                {
                    SceneManager.LoadScene("Game");
                }
            }
            if (Input.GetButton("Select" + i.ToString()))
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
}
