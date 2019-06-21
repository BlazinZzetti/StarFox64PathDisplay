using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EstPercentCalc : MonoBehaviour
{
    public ARankTracker ARanks;
    public KeyTracker Keys;
    public PathTracker Paths;
    public Text DisplayText;

    // Update is called once per frame
    void Update()
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

        var totalMission = 71;
        var totalKeys = 115 * 2;
        var totalPaths = 326 * 4;

        DisplayText.text = (((ARanks.totalMissionsCompleted + (Keys.numOfKeys * 2) + (Paths.numOfPaths * 3)) / (float)(totalMission + totalKeys + totalPaths)) * 100).ToString("f2") + "%";
    }
}
