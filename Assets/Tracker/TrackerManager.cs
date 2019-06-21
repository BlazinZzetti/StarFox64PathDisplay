using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class TrackerManager : MonoBehaviour
{
    public Text ARankTrackerText;
    public Text KeyTrackerText;
    public Text PathTrackerText;
    public Text TotalTrackerText;
    public Text EstimatedTrackerText;

    public Toggle ARanksToggle;
    public Toggle KeysToggle;
    public Toggle PathsToggle;
    public Toggle TotalToggle;
    public Toggle EstToggle;
    public Toggle AutoSaveToggle;

    //Path Tracker Variables
    int numOfPaths = 0;
    public PathViewerControl PathControl;
    public Canvas PathControlMenu;

    public Canvas MenuCamera;

    [HideInInspector]
    public string storedFilePath = string.Empty;

    //Key Tracker Variables
    int numOfKeys = 0;
    private bool KeyEditMode = false;

    //A Rank Tracker Variables
    List<ThreeMissionControl> ThreeMissionLevels = new List<ThreeMissionControl>();
    List<TwoMissionControl> TwoMissionLevels = new List<TwoMissionControl>();
    int totalMissionsCompleted = 0;

    #region Mission and Boss Controls
    public MissionControl WestopolisControl;

    public MissionControl DigitalCircuitControl;
    public MissionControl GlyphicCanyonControl;
    public MissionControl LethalHighwayControl;

    public MissionControl CrypticCastleControl;
    public MissionControl PrisonIslandControl;
    public MissionControl CircusParkControl;

    public MissionControl CentralCityControl;
    public MissionControl TheDoomControl;
    public MissionControl SkyTroopsControl;
    public MissionControl MadMatrixControl;
    public MissionControl DeathRuinsControl;

    public MissionControl TheARKControl;
    public MissionControl AirFleetControl;
    public MissionControl IronJungleControl;
    public MissionControl SpaceGadgetControl;
    public MissionControl LostImpactControl;

    public MissionControl GUNFortressControl;
    public MissionControl BlackCometControl;
    public MissionControl LavaShelterControl;
    public MissionControl CosmicFallControl;
    public MissionControl FinalHauntControl;

    public MissionControl TheLastWayControl;

    public List<BossControl> Bosses = new List<BossControl>(); 
    #endregion

    // Use this for initialization
    void Start ()
    {
        ThreeMissionLevels = new List<ThreeMissionControl> { (ThreeMissionControl)WestopolisControl,
                                                             (ThreeMissionControl)GlyphicCanyonControl,
                                                             (ThreeMissionControl)CrypticCastleControl,
                                                             (ThreeMissionControl) PrisonIslandControl,
                                                             (ThreeMissionControl)CircusParkControl,
                                                             (ThreeMissionControl)TheDoomControl,
                                                             (ThreeMissionControl)SkyTroopsControl,
                                                             (ThreeMissionControl)MadMatrixControl,
                                                             (ThreeMissionControl)AirFleetControl,
                                                             (ThreeMissionControl)IronJungleControl,
                                                             (ThreeMissionControl)SpaceGadgetControl };

        TwoMissionLevels = new List<TwoMissionControl> { (TwoMissionControl)DigitalCircuitControl,
                                                        (TwoMissionControl)LethalHighwayControl,
                                                        (TwoMissionControl)CentralCityControl,
                                                        (TwoMissionControl)DeathRuinsControl,
                                                        (TwoMissionControl)TheARKControl,
                                                        (TwoMissionControl)LostImpactControl,
                                                        (TwoMissionControl)GUNFortressControl,
                                                        (TwoMissionControl)BlackCometControl,
                                                        (TwoMissionControl)LavaShelterControl,
                                                        (TwoMissionControl)CosmicFallControl,
                                                        (TwoMissionControl)FinalHauntControl };

        //StartCoroutine("LoadFileFirstTime");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Path Options
        if (Input.GetKeyUp(KeyCode.P))
        {
            togglePathMenu();
        }

        //Set All Missions and Keys to Obtained
        if (Input.GetKeyDown(KeyCode.A))
        {
            setObtainAllNonPathItems();
        }

        //Open Keys Menu
        if (Input.GetKeyUp(KeyCode.K))
        {
            toggleKeyMenu();
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            showRemainingMission();
        }

        //Return to Main Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuCamera.enabled = !MenuCamera.enabled;
        }

        updateARanks();

        updateKeys();

        updatePaths();

        updateTrackerTextObjects();

        updateTrackersEnabled();

    }

    void updateARanks()
    {
        totalMissionsCompleted = 0;
        totalMissionsCompleted += checkThreeMissionLevelsComplete();
        totalMissionsCompleted += checkTwoMissionLevelsComplete();

        foreach (var boss in Bosses)
        {
            if (boss.BossComplete.enabled)
            {
                totalMissionsCompleted++;
            }
        }
    }

    void updateKeys()
    {
        numOfKeys = 0;
        
        foreach (var level in ThreeMissionLevels)
        {
            numOfKeys += level.NumOfKeys;
        }

        foreach (var level in TwoMissionLevels)
        {
            numOfKeys += level.NumOfKeys;
        }

        numOfKeys += TheLastWayControl.NumOfKeys;
    }

    void updatePaths()
    {
        numOfPaths = 0;
        foreach (PathControlItem path in PathControl.Paths)
        {
            if (path.CompletedToggle.isOn)
            {
                numOfPaths++;
            }
        }
    }

    void updateTrackerTextObjects()
    {
        //Estimated Tracker Weights
        var totalPaths = 326 * 4;

        ARankTrackerText.text =     "A Ranks: " + totalMissionsCompleted + " / 71 " + ((totalMissionsCompleted / 71.0f) * 100).ToString("f2") + "%";
        KeyTrackerText.text =       "Keys: " + numOfKeys + " / 115 " + ((numOfKeys / 115) * 100).ToString("f2") + "%";
        PathTrackerText.text =      "Paths: " + numOfPaths + " / 326 " + ((numOfPaths / 326.0f) * 100).ToString("f2") + "%";
        TotalTrackerText.text =     "Total: " + (totalMissionsCompleted + numOfKeys + numOfPaths) + " / 512 " + (((totalMissionsCompleted + numOfKeys + numOfPaths) / 512.0f) * 100).ToString("f2") + "%";
        EstimatedTrackerText.text = (((totalMissionsCompleted + (numOfKeys) + (numOfPaths * 4)) / (float)(71 + 115 + totalPaths)) * 100).ToString("f2") + "%";
    }

    void updateTrackersEnabled()
    {
        ARankTrackerText.enabled = ARanksToggle.isOn;
        KeyTrackerText.enabled = KeysToggle.isOn;
        PathTrackerText.enabled = PathsToggle.isOn;
        TotalTrackerText.enabled = TotalToggle.isOn;
        
        EstimatedTrackerText.enabled = EstToggle.isOn;
        EstimatedTrackerText.transform.GetChild(0).gameObject.SetActive(EstToggle.isOn);
    }

    private IEnumerator LoadFileFirstTime()
    {
        while (PathControl.numOfPaths != 326)
        {
            yield return new WaitForSeconds(0.5f);
        }
        LoadData();
    }

    private void SaveData()
    {
        if (!string.IsNullOrEmpty(storedFilePath))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(storedFilePath))
            {
                foreach (var path in PathControl.Paths)
                {
                    sw.WriteLine(path.CompletedToggle.isOn);
                }
            }
        }
    }

    private void LoadData()
    {
        if (File.Exists(storedFilePath))
        {
            // Open the file to read from.
            using (StreamReader sr = File.OpenText(storedFilePath))
            {
                string s = "";
                foreach (var path in PathControl.Paths)
                {
                    if ((s = sr.ReadLine()) != null)
                    {
                        path.CompletedToggle.isOn = bool.Parse(s);
                    }
                }
            }
        }
    }

    void showRemainingMission()
    {
        foreach (var level in ThreeMissionLevels)
        {
            level.ResetRemaining();
            level.ShowRemainingText(!level.RemainingDarkText.enabled);
        }

        foreach (var level in TwoMissionLevels)
        {
            level.ResetRemaining();
            level.ShowRemainingText(!level.RemainingDarkText.enabled);
        }

        foreach (var path in PathControl.Paths.Where(p => !p.CompletedToggle.isOn).ToList())
        {
            for (int i = 0; i < path.LevelsInPath.Count - 1; i++)
            {
                var level = path.LevelsInPath[i];

                if (level.NextLevelHero != null && level.LevelControl != null)
                {
                    if (level.NextLevelHero.Equals(path.LevelsInPath[i + 1]))
                    {
                        level.LevelControl.RemainingHeroPlays++;
                    }
                }
                if (level.NextLevelNeutral != null && level.LevelControl != null)
                {
                    if (level.NextLevelNeutral.Equals(path.LevelsInPath[i + 1]))
                    {
                        level.LevelControl.RemainingNeutralPlays++;
                    }
                }
                if (level.NextLevelDark != null && level.LevelControl != null)
                {
                    if (level.NextLevelDark.Equals(path.LevelsInPath[i + 1]))
                    {
                        level.LevelControl.RemainingDarkPlays++;
                    }
                }
            }
        }

        foreach (var level in ThreeMissionLevels)
        {
            level.UpdateRemainingText();
        }

        foreach (var level in TwoMissionLevels)
        {
            level.UpdateRemainingText();
        }
    }

    int checkThreeMissionLevelsComplete()
    {
        int missionsCompleted = 0;
        foreach (var mission in ThreeMissionLevels)
        {
            if (mission.DarkComplete.enabled)
            {
                missionsCompleted++;
            }
            if (mission.NeutralComplete.enabled)
            {
                missionsCompleted++;
            }
            if (mission.HeroComplete.enabled)
            {
                missionsCompleted++;
            }
        }

        return missionsCompleted;
    }

    int checkTwoMissionLevelsComplete()
    {
        int missionsCompleted = 0;
        foreach (var mission in TwoMissionLevels)
        {
            switch (mission.Mode)
            {
                case TwoMissionControl.TwoMissionMode.DarkHeroTop:
                case TwoMissionControl.TwoMissionMode.DarkHeroBottom:
                case TwoMissionControl.TwoMissionMode.DarkHeroLast:
                    if (mission.DarkComplete.enabled)
                    {
                        missionsCompleted++;
                    }
                    if (mission.HeroComplete.enabled)
                    {
                        missionsCompleted++;
                    }
                    break;
                case TwoMissionControl.TwoMissionMode.DarkNeutral:
                    if (mission.DarkComplete.enabled)
                    {
                        missionsCompleted++;
                    }
                    if (mission.NeutralDarkComplete.enabled)
                    {
                        missionsCompleted++;
                    }
                    break;
                case TwoMissionControl.TwoMissionMode.NeutralHero:
                    if (mission.NeutralHeroComplete.enabled)
                    {
                        missionsCompleted++;
                    }
                    if (mission.HeroComplete.enabled)
                    {
                        missionsCompleted++;
                    }
                    break;
            }
        }

        return missionsCompleted;
    }

    void setObtainAllNonPathItems()
    {
        foreach (var level in ThreeMissionLevels)
        {
            level.SetAllComplete();
            level.NumOfKeys = 5;
        }

        foreach (var level in TwoMissionLevels)
        {
            level.SetAllComplete();
            level.NumOfKeys = 5;
        }

        TheLastWayControl.NumOfKeys = 5;

        foreach (var boss in Bosses)
        {
            boss.BossComplete.enabled = true;
        }       
    }

    void toggleKeyMenu()
    {
        KeyEditMode = !KeyEditMode;
        foreach (var level in ThreeMissionLevels)
        {
            level.KeyEditMode.SetActive(KeyEditMode);
            level.SetPathActives(!KeyEditMode);
        }

        foreach (var level in TwoMissionLevels)
        {
            level.KeyEditMode.SetActive(KeyEditMode);
            level.SetPathActives(!KeyEditMode);
        }

        TheLastWayControl.KeyEditMode.SetActive(KeyEditMode);
        TheLastWayControl.SetPathActives(!KeyEditMode);
    }

    void togglePathMenu()
    {
        if (PathControlMenu.enabled && AutoSaveToggle.isOn)
        {
            SaveData();
        }
        PathControlMenu.enabled = !PathControlMenu.enabled;
    }
}
