using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class KeyTracker : MonoBehaviour
{
    public List<MissionControl> levels = new List<MissionControl>();
    public Text DisplayText;
    [HideInInspector]
    public int numOfKeys = 0;
    [HideInInspector]
    public int totalPossibleKeys = 0;

    private bool KeyEditMode = false;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            KeyEditMode = !KeyEditMode;
            foreach (var level in levels)
            {
                level.KeyEditMode.SetActive(KeyEditMode);
                level.SetPathActives(!KeyEditMode);
            }
        }

        numOfKeys = 0;
        totalPossibleKeys = levels.Count * 5;
        foreach (var level in levels)
        {
            numOfKeys += level.NumOfKeys;
        }
        DisplayText.text = "Keys: " + numOfKeys + " / " + (levels.Count * 5) + " " + ((numOfKeys / (float)totalPossibleKeys) * 100).ToString("f2") + "%";
    }

}
