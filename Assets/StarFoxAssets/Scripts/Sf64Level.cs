using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sf64Level : MonoBehaviour
{
    public GameObject BlueLine;
    public GameObject RedLine;
    public GameObject YellowLine;
    public GameObject WarpLine;

    public enum ExitsType
    {
        Red,
        Blue,
        Yellow,
        BlueRed,
        RedYellow,
        BlueYellow,
        BlueWarp
    }

    public ExitsType LevelExits = ExitsType.Red;
}
