using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sf64Level : MonoBehaviour
{
    public GameObject BlueLine;
    public GameObject RedLine;
    public GameObject YellowLine;
    public GameObject WarpLine;

    public bool MedelAquired = false;

    public enum ExitsType
    {
        Red,
        Blue,
        Yellow,
        BlueRed,
        RedYellow,
        BlueYellow,
        BlueWarp,
        BlueYellowWarp
    }

    public ExitsType LevelExits = ExitsType.Red;

    public void ShowBlueLine(bool show)
    {
        BlueLine.GetComponent<Renderer>().enabled = show;
    }
    public void ShowRedLine(bool show)
    {
        RedLine.GetComponent<Renderer>().enabled = show;
    }
    public void ShowYellowLine(bool show)
    {
        YellowLine.GetComponent<Renderer>().enabled = show;
    }
    public void ShowWarpLine(bool show)
    {
        WarpLine.GetComponent<Renderer>().enabled = show;
    }
}
