using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SFPathControlItem : MonoBehaviour 
{
	public Text Label;
	public Toggle CompletedToggle;
    public Toggle ShowToggle;
	public List<SFLevel> LevelsInPath = new List<SFLevel>();

	// Use this for initialization
	void Start () 
	{
        ShowToggle.onValueChanged.AddListener(delegate { UpdatePathDisplay(); });
	}
	
	// Update is called once per frame
	void UpdatePathDisplay() 
	{
        for (int i = 0; i < LevelsInPath.Count - 1; i++)
        {
            if (LevelsInPath[i].BlueLine != null && LevelsInPath[i].LevelControl != null)
            {
                if (LevelsInPath[i].BlueLine.Equals(LevelsInPath[i + 1]))
                {
                    LevelsInPath[i].LevelControl.ShowBlueLine(ShowToggle.isOn);
                }
            }
            if (LevelsInPath[i].RedLine != null && LevelsInPath[i].LevelControl != null)
            {
                if (LevelsInPath[i].RedLine.Equals(LevelsInPath[i + 1]))
                {
                    LevelsInPath[i].LevelControl.ShowRedLine(ShowToggle.isOn);
                }
            }
            if (LevelsInPath[i].YellowLine != null && LevelsInPath[i].LevelControl != null)
            {
                if (LevelsInPath[i].YellowLine.Equals(LevelsInPath[i + 1]))
                {
                    LevelsInPath[i].LevelControl.ShowYellowLine(ShowToggle.isOn);
                }
            }
            if (LevelsInPath[i].WarpLine != null && LevelsInPath[i].LevelControl != null)
            {
                if (LevelsInPath[i].WarpLine.Equals(LevelsInPath[i + 1]))
                {
                    LevelsInPath[i].LevelControl.ShowWarpLine(ShowToggle.isOn);
                }
            }
        }
	}

    public void Setup(string text, List<SFLevel> levelsInPath)
    {
        Label.text = text;
        LevelsInPath = new List<SFLevel>(levelsInPath);
    }
}
