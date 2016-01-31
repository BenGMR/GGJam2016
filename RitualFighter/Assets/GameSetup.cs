using UnityEngine;
using System.Collections;
using System.Linq;

public class GameSetup : MonoBehaviour {

    public RaggedySpineboy leftPlayer;
    public RaggedySpineboy rightPlayer;

	void Start ()
    {
        leftPlayer.player = (Player)(int.Parse(TeamSelectionScript.instance.leftTeam[0].name.Last().ToString()));
        leftPlayer.player2 = TeamSelectionScript.instance.leftTeam.Count > 1 ? (Player)(int.Parse(TeamSelectionScript.instance.leftTeam[1].name.Last().ToString())) : leftPlayer.player;
        rightPlayer.player = (Player)(int.Parse(TeamSelectionScript.instance.rightTeam[0].name.Last().ToString()));
        rightPlayer.player2 = TeamSelectionScript.instance.rightTeam.Count > 1 ? (Player)(int.Parse(TeamSelectionScript.instance.rightTeam[1].name.Last().ToString())) : rightPlayer.player;
    }
}
