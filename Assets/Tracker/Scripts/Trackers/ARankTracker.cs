using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ARankTracker : MonoBehaviour
{
    public List<ThreeMissionControl> ThreeMissionLevels = new List<ThreeMissionControl>();
    public List<TwoMissionControl> TwoMissonLevels = new List<TwoMissionControl>();
    public List<BossControl> Bosses = new List<BossControl>();
    public Text DisplayText;

    [HideInInspector]
    public int totalMissionsCompleted = 0;

    // Use this for initialization
    void Start ()
    {
        if (ThreeMissionLevels.Count != 11 && TwoMissonLevels.Count != 11 && Bosses.Count != 16) //11 *3 + 11 *2 + 16
        {
            Debug.LogError("One or more lists have too many or too few items");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        totalMissionsCompleted = 0;
        totalMissionsCompleted += CheckThreeMissionLevelsComplete();
        totalMissionsCompleted += CheckTwoMissionLevelsComplete();
        foreach (var boss in Bosses)
        {
            if (boss.BossComplete.enabled)
            {
                totalMissionsCompleted++;
            }
        }

        DisplayText.text = "A Ranks: " + totalMissionsCompleted + " / 71 " + ((totalMissionsCompleted / 71.0f)*100).ToString("f2") + "%";

    }

    int CheckThreeMissionLevelsComplete()
    {
        int missionsCompleted = 0;
        foreach (var mission in ThreeMissionLevels)
        {
            if(mission.DarkComplete.enabled)
            {
                missionsCompleted++;
            }
            if(mission.NeutralComplete.enabled)
            {
                missionsCompleted++;
            }
            if(mission.HeroComplete.enabled)
            {
                missionsCompleted++;
            }
        }

        return missionsCompleted;
    }

    int CheckTwoMissionLevelsComplete()
    {
        int missionsCompleted = 0;
        foreach (var mission in TwoMissonLevels)
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
}
