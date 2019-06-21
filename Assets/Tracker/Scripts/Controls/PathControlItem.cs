using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PathControlItem : MonoBehaviour 
{
	public Text Label;
	public Toggle CompletedToggle;
	public Toggle ShowToggle;
    public string PathCode = "XXXXXX";
	public List<Level> LevelsInPath = new List<Level>();

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
            if (LevelsInPath[i].NextLevelHero != null && LevelsInPath[i].LevelControl != null)
            {
                if (LevelsInPath[i].NextLevelHero.Equals(LevelsInPath[i + 1]))
                {
                    LevelsInPath[i].LevelControl.ShowHeroPath(ShowToggle.isOn);
                }
            }
            if (LevelsInPath[i].NextLevelNeutral != null && LevelsInPath[i].LevelControl != null)
            {
                if (LevelsInPath[i].NextLevelNeutral.Equals(LevelsInPath[i + 1]))
                {
                    LevelsInPath[i].LevelControl.ShowNormalPath(ShowToggle.isOn);
                }
            }
            if (LevelsInPath[i].NextLevelDark != null && LevelsInPath[i].LevelControl != null)
            {
                if (LevelsInPath[i].NextLevelDark.Equals(LevelsInPath[i + 1]))
                {
                    LevelsInPath[i].LevelControl.ShowDarkPath(ShowToggle.isOn);
                }
            }
        }
	}

    public void Setup(string text, List<Level> levelsInPath)
    {
        Label.text = text;
        LevelsInPath = new List<Level>(levelsInPath);
        PathCode = "";
        for (int i = 0; i < LevelsInPath.Count - 1; i++)
        {
            if (LevelsInPath[i].NextLevelHero != null && LevelsInPath[i].LevelControl != null)
            {
                if (LevelsInPath[i].NextLevelHero.Equals(LevelsInPath[i + 1]))
                {
                    PathCode += "H";
                }
            }
            if (LevelsInPath[i].NextLevelNeutral != null && LevelsInPath[i].LevelControl != null)
            {
                if (LevelsInPath[i].NextLevelNeutral.Equals(LevelsInPath[i + 1]))
                {
                    PathCode += "N";
                }
            }
            if (LevelsInPath[i].NextLevelDark != null && LevelsInPath[i].LevelControl != null)
            {
                if (LevelsInPath[i].NextLevelDark.Equals(LevelsInPath[i + 1]))
                {
                    PathCode += "D";
                }
            }
        }
    }
}
