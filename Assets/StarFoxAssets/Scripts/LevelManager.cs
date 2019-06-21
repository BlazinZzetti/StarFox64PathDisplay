using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Text MedelsTrackerText;
    public Text PathTrackerText;

    public Toggle MedelsToggle;
    public Toggle PathsToggle;
    public Toggle AutoSaveToggle;

    //Path Tracker Variables
    int numOfPaths = 0;
    public SFPathViewerControl PathControl;
    public Canvas PathControlMenu;

    int totalMedelAquired = 0;

    public Canvas MenuCamera;

    [HideInInspector]
    public string storedFilePath = string.Empty;

    public Sf64Level Corneria;
    public Sf64Level Metro;
    public Sf64Level SectorY;
    public Sf64Level Katina;
    public Sf64Level Fichina;
    public Sf64Level Solar;
    public Sf64Level Aquas;
    public Sf64Level SectorX;
    public Sf64Level Zoness;
    public Sf64Level SectorZ;
    public Sf64Level Titania;
    public Sf64Level Macbeth;
    public Sf64Level Area6;
    public Sf64Level Bolse;
    public Sf64Level Venom;

    List<Sf64Level> Levels = new List<Sf64Level>();

    // Use this for initialization
    void Start()
    {
        Levels = new List<Sf64Level>
    {
        Corneria,
        Metro,
        SectorY,
        Katina,
        Fichina,
        Solar,
        Aquas,
        SectorX,
        Zoness,
        SectorZ,
        Titania,
        Macbeth,
        Area6,
        Bolse,
        Venom
    };

    }
    // Update is called once per frame
    void Update()
    {
        //Path Options
        if (Input.GetKeyUp(KeyCode.P))
        {
            togglePathMenu();
        }

        //if (Input.GetKeyUp(KeyCode.Z))
        //{
        //    showRemainingMission();
        //}

        //Return to Main Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuCamera.enabled = !MenuCamera.enabled;
        }

        updateMedels();

        updatePaths();

        updateTrackerTextObjects();

        updateTrackersEnabled();

    }

    void updateMedels()
    {
        totalMedelAquired = 0;
        totalMedelAquired += checkMedelsAquired();
    }

    void updatePaths()
    {
        numOfPaths = 0;
        foreach (SFPathControlItem path in PathControl.Paths)
        {
            if (path.CompletedToggle.isOn)
            {
                numOfPaths++;
            }
        }
    }

    void updateTrackerTextObjects()
    {
        MedelsTrackerText.text = "Medels: " + totalMedelAquired + " / " + Levels.Count + " " + ((totalMedelAquired / (float)Levels.Count) * 100).ToString("f2") + "%";
        PathTrackerText.text = "Paths: " + numOfPaths + " / " + PathControl.Paths.Count + " " + ((numOfPaths / (float)PathControl.Paths.Count) * 100).ToString("f2") + "%";
    }

    void updateTrackersEnabled()
    {
        MedelsTrackerText.enabled = MedelsToggle.isOn;
        PathTrackerText.enabled = PathsToggle.isOn;
    }

    void togglePathMenu()
    {
        if (PathControlMenu.enabled && AutoSaveToggle.isOn)
        {
            SaveData();
        }
        PathControlMenu.enabled = !PathControlMenu.enabled;
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

    private int checkMedelsAquired()
    {
        int missionsCompleted = 0;
        foreach (var mission in Levels)
        {
            if (mission.MedelAquired)
            {
                missionsCompleted++;
            }
        }
        return missionsCompleted;
    }

    private IEnumerator LoadFileFirstTime()
    {
        while (PathControl.numOfPaths != 326)
        {
            yield return new WaitForSeconds(0.5f);
        }
        LoadData();
    }

    //void showRemainingMission()
    //{
    //    foreach (var level in ThreeMissionLevels)
    //    {
    //        level.ResetRemaining();
    //        level.ShowRemainingText(!level.RemainingDarkText.enabled);
    //    }

    //    foreach (var level in TwoMissionLevels)
    //    {
    //        level.ResetRemaining();
    //        level.ShowRemainingText(!level.RemainingDarkText.enabled);
    //    }

    //    foreach (var path in PathControl.Paths.Where(p => !p.CompletedToggle.isOn).ToList())
    //    {
    //        for (int i = 0; i < path.LevelsInPath.Count - 1; i++)
    //        {
    //            var level = path.LevelsInPath[i];

    //            if (level.NextLevelHero != null && level.LevelControl != null)
    //            {
    //                if (level.NextLevelHero.Equals(path.LevelsInPath[i + 1]))
    //                {
    //                    level.LevelControl.RemainingHeroPlays++;
    //                }
    //            }
    //            if (level.NextLevelNeutral != null && level.LevelControl != null)
    //            {
    //                if (level.NextLevelNeutral.Equals(path.LevelsInPath[i + 1]))
    //                {
    //                    level.LevelControl.RemainingNeutralPlays++;
    //                }
    //            }
    //            if (level.NextLevelDark != null && level.LevelControl != null)
    //            {
    //                if (level.NextLevelDark.Equals(path.LevelsInPath[i + 1]))
    //                {
    //                    level.LevelControl.RemainingDarkPlays++;
    //                }
    //            }
    //        }
    //    }

    //    foreach (var level in ThreeMissionLevels)
    //    {
    //        level.UpdateRemainingText();
    //    }

    //    foreach (var level in TwoMissionLevels)
    //    {
    //        level.UpdateRemainingText();
    //    }
    //}
}