using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Examples.PhysicsPickupParty
{
    public class SceneReference : MonoBehaviour
    {
        // We will use a non-networked object/script, to hold all our references to other scripts, objects and UI.
        // As networked objects may be disabled in certain situations.

        public PlayerPickupParty playerPickupParty;
        public TeamManager teamManager;
        public ZonesManager zonesManager;
        public Text gameStartTimer, roundEndTimer;
        public Button skipGameStartTimerButton;
        public GameObject panelControls, panelInfo, panelGameStartTimer, panelRoundEndTimer, panelEndGame;
        public GameObject skipGameStartTimerObj, winObj, loseObj;
        public Image[] UIBackgrounds;

        public Text[] scoresTeam;
        public Image[] scoresTeamImageColour;
        

        private void Start()
        {
            skipGameStartTimerButton.onClick.AddListener(SkipGameStartTimerButton);

            skipGameStartTimerObj.SetActive(false);
            panelEndGame.SetActive(false);
            loseObj.SetActive(false);
            winObj.SetActive(false);
        }

        public void SetUIBGTeamColour(int _team)
        {
            //print("SetUIBGTeamColour: " + _team);
            foreach (Image item in UIBackgrounds)
            {
                item.color = teamManager.teamColours[_team];
            }
        }

        public void SkipGameStartTimerButton()
        {
            if (teamManager.isServer)
            {
                teamManager.gameStartTime = 0;
            }
        }

        public void UpdateScoresUI(int _index, int _teamID, int _score)
        {
            scoresTeam[_index].text = _score.ToString();
            scoresTeamImageColour[_index ].color = teamManager.teamColours[_teamID];
        }

        public void EndGameUI()
        {
            // no draw detection, even if 1st and 2nd have same result, low priority
            if (zonesManager.zoneResultsListTuple.Count > 0 && playerPickupParty.teamID == zonesManager.zoneResultsListTuple[0].Item2)
            {
                panelEndGame.SetActive(true);
                winObj.SetActive(true);
            }
            else
            {
                panelEndGame.SetActive(true);
                loseObj.SetActive(true);
            }
        }
    }
}