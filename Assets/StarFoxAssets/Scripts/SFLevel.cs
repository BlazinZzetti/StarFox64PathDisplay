using UnityEngine;
using System.Collections;

public class SFLevel
{
    public Sf64Level LevelControl;
	public SFLevel BlueLine;
	public SFLevel RedLine;
	public SFLevel YellowLine;
    public SFLevel WarpLine;

    public GameObject BlueLineLevel;
    public GameObject RedLineLevel;
    public GameObject YellowLineLevel;
    public GameObject WarpLineLevel;

    public enum ExitsType
    {
        Red,
        Blue,
        Yellow,
        BlueRed,
        RedYellow,
        BlueYellow,
        BlueWarp,
        BlueYellowWarp,
        Venom
    }

    public ExitsType levelMode = ExitsType.BlueRed;

    public SFLevel(Sf64Level levelControl, ExitsType mode, SFLevel blue, SFLevel red, SFLevel yellow, SFLevel warp)
	{
        LevelControl = levelControl;
        BlueLine = blue;
        RedLine = red;
        YellowLine = yellow;
        WarpLine = warp;
        levelMode = mode;
	}
}
