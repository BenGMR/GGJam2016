using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public enum Team
{
    left,
    right,
    none
}

public class TeamSelectionScript : MonoBehaviour
{
    public Controller p1, p2, p3, p4;
    public Controller[] players = new Controller[4];
    public bool Startable = false;
    public Button playButton;
    public Image WarningMessage;
    bool showMessage;
    float messageAlpha;

    // Use this for initialization
    void Start()
    {
        players[0] = p1;
        players[1] = p2;
        players[2] = p3;
        players[3] = p4;

        showMessage = false;
        messageAlpha = 0f;
        Startable = false;
        TeamInfo.Teams.Clear();
        
        TeamInfo.Teams.Add(Team.left, new List<Player>());
        TeamInfo.Teams.Add(Team.right,new List<Player>());
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].Ready)
            {
                if (players[i].team == Team.left && !TeamInfo.Teams[Team.left].Contains((Player)(i+1)))
                {
                    if (TeamInfo.Teams[Team.left].Count < 2)
                    {
                        TeamInfo.Teams[Team.left].Add((Player) (i+1));
                    }
                }
                if (players[i].team == Team.right && !TeamInfo.Teams[Team.right].Contains((Player)(i + 1)))
                {
                    if (TeamInfo.Teams[Team.right].Count < 2)
                    {
                        TeamInfo.Teams[Team.right].Add((Player)(i + 1));
                    }
                }
            }
            else
            {
                if (TeamInfo.Teams[Team.left].Contains((Player)(i+1)))
                {
                    TeamInfo.Teams[Team.left].Remove((Player)(i + 1));
                }
                else if (TeamInfo.Teams[Team.right].Contains((Player)(i + 1)))
                {
                    TeamInfo.Teams[Team.left].Remove((Player)(i + 1));
                }
            }
        }
        if (TeamInfo.Teams[Team.left].Count >= 1 && TeamInfo.Teams[Team.right].Count >= 1 && TeamInfo.Teams[Team.left].Count < 3 && TeamInfo.Teams[Team.right].Count < 3)
        {
            Startable = true;

            playButton.enabled = true;
        }
        else if (TeamInfo.Teams[Team.left].Count >= 3 || TeamInfo.Teams[Team.right].Count >= 3)
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
                    CanvasManager.scenes.Clear();
                    CanvasManager.scenes.Push(SceneManager.GetSceneByName("Game"));
                    SceneManager.LoadScene("Game", LoadSceneMode.Single);
                }
            }
            if (Input.GetButton("Select" + i.ToString()))
            {
                CanvasManager.scenes.Clear();
                CanvasManager.scenes.Push(SceneManager.GetSceneByName("Title"));
                SceneManager.LoadScene("Title", LoadSceneMode.Single);
            }
        }
    }
}
