using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PercentageCalc : MonoBehaviour
{
    public ARankTracker ARanks;
    public KeyTracker Keys;
    public PathTracker Paths;
    public Text DisplayText;
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (var level in ARanks.ThreeMissionLevels)
            {
                level.SetAllComplete();
            }
            foreach (var level in ARanks.TwoMissonLevels)
            {
                level.SetAllComplete();
            }
            foreach (var boss in ARanks.Bosses)
            {
                boss.BossComplete.enabled = true;
            }
            foreach (var level in Keys.levels)
            {
                level.NumOfKeys = 5;
            }
        }

        DisplayText.text = "Total: " + (ARanks.totalMissionsCompleted + Keys.numOfKeys + Paths.numOfPaths) + " / 507 " + (((ARanks.totalMissionsCompleted + Keys.numOfKeys + Paths.numOfPaths) / 507.0f) * 100).ToString("f2") + "%";
    }
}
