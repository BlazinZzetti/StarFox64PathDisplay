using UnityEngine;
using System.Collections.Generic;

public class IconDisplayModeControl : MonoBehaviour
{
    public List<MissionControl> Levels;
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Levels.Count > 0)
            {
                foreach (var level in Levels)
                {
                    if (level.LevelIcon != null)
                    {
                        level.LevelIcon.SetActive(!level.LevelIcon.activeSelf);
                    }
                }
            }
        }
	}
}
