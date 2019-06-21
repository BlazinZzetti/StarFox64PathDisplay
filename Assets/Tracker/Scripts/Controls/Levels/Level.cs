using UnityEngine;
using System.Collections;

public class Level
{
    public MissionControl LevelControl;
	public Level NextLevelDark;
	public Level NextLevelNeutral;
	public Level NextLevelHero;

    public enum LevelMode { AllMissions, HeroDark, HeroNeutral, DarkNeutral, FinalLevel}

    public LevelMode levelMode = LevelMode.AllMissions;

    public Level(MissionControl levelControl, LevelMode mode, Level dark, Level neutral, Level hero)
	{
        LevelControl = levelControl;
		NextLevelHero = hero;
		NextLevelNeutral = neutral;
		NextLevelDark = dark;
        levelMode = mode;
	}
}
