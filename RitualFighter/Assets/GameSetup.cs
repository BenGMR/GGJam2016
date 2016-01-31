using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class GameSetup : MonoBehaviour {

    public RaggedySpineboy leftPlayer;
    public RaggedySpineboy rightPlayer;

	void Start ()
    {
        leftPlayer.player = TeamInfo.Teams[Team.left][0];
        leftPlayer.player2 = TeamInfo.Teams[Team.left].Count > 1 ? TeamInfo.Teams[Team.left][1] : leftPlayer.player;

        rightPlayer.player = TeamInfo.Teams[Team.right][0];
        rightPlayer.player2 = TeamInfo.Teams[Team.right].Count > 1 ? TeamInfo.Teams[Team.right][1] : rightPlayer.player;
    }
}
