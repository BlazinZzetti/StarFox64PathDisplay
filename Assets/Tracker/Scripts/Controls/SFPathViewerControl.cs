using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SFPathViewerControl : MonoBehaviour
{
    public ScrollRect ScrollView;

    public List<SFPathControlItem> Paths = new List<SFPathControlItem>();

    public List<SFLevel> LevelPath = new List<SFLevel>();

    //Used to determine when the path control items are fully loaded.
    [HideInInspector]
    public int numOfPaths = 0;

    public LevelManager Manager;

    // Use this for initialization
    void Start()
    {
        numOfPaths = 0;

        SFLevel Venom = new SFLevel(Manager.Venom, SFLevel.ExitsType.Venom, null, null, null, null);
        SFLevel Area6 = new SFLevel(Manager.Area6, SFLevel.ExitsType.Red, null, Venom, null, null);
        SFLevel Bolse = new SFLevel(Manager.Bolse, SFLevel.ExitsType.Blue, Venom, null, null, null);

        SFLevel Macbeth = new SFLevel(Manager.Macbeth, SFLevel.ExitsType.BlueRed, Bolse, Area6, null, null);
        SFLevel SectorZ = new SFLevel(Manager.SectorZ, SFLevel.ExitsType.BlueRed, Bolse, Area6, null, null);
        SFLevel Titaina = new SFLevel(Manager.Titania, SFLevel.ExitsType.Blue, Bolse, null, null, null);

        SFLevel SectorX = new SFLevel(Manager.SectorX, SFLevel.ExitsType.BlueYellowWarp, Titaina, null, Macbeth, SectorZ);
        SFLevel Zoness = new SFLevel(Manager.Zoness, SFLevel.ExitsType.RedYellow, null, SectorZ, Macbeth, null);
        SFLevel Solar = new SFLevel(Manager.Solar, SFLevel.ExitsType.Yellow, null, null, Macbeth, null);

        SFLevel Katina = new SFLevel(Manager.Katina, SFLevel.ExitsType.BlueYellow, SectorX, null, Solar, null);
        SFLevel Aquas = new SFLevel(Manager.Aquas, SFLevel.ExitsType.Red, null, Zoness, null, null);
        SFLevel Fichina = new SFLevel(Manager.Fichina, SFLevel.ExitsType.BlueYellow, SectorX, null, Solar, null);

        SFLevel SectorY = new SFLevel(Manager.SectorY, SFLevel.ExitsType.RedYellow, null, Aquas, Katina, null);
        SFLevel Meteo = new SFLevel(Manager.Metro, SFLevel.ExitsType.BlueWarp, Fichina, null, null, Katina);

        SFLevel Cornaria = new SFLevel(Manager.Corneria, SFLevel.ExitsType.BlueRed, Meteo, SectorY, null, null);

        LevelSearch(Cornaria);
    }

    public void LevelSearch(SFLevel level)
    {
        LevelPath.Add(level);
        switch (level.levelMode)
        {
            case SFLevel.ExitsType.Blue:
                if (level.BlueLine != null)
                {
                    LevelSearch(level.BlueLine);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                break;
            case SFLevel.ExitsType.Red:
                if (level.RedLine != null)
                {
                    LevelSearch(level.RedLine);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                break;
            case SFLevel.ExitsType.Yellow:
                if (level.YellowLine != null)
                {
                    LevelSearch(level.YellowLine);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                break;
            case SFLevel.ExitsType.BlueRed:
                if (level.BlueLine != null)
                {
                    LevelSearch(level.BlueLine);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                if (level.RedLine != null)
                {
                    LevelSearch(level.RedLine);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                break;
            case SFLevel.ExitsType.BlueWarp:
                if (level.BlueLine != null)
                {
                    LevelSearch(level.BlueLine);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                if (level.WarpLine != null)
                {
                    LevelSearch(level.WarpLine);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                break;
            case SFLevel.ExitsType.BlueYellow:
                if (level.BlueLine != null)
                {
                    LevelSearch(level.BlueLine);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                if (level.YellowLine != null)
                {
                    LevelSearch(level.YellowLine);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                break;
            case SFLevel.ExitsType.BlueYellowWarp:
                if (level.BlueLine != null)
                {
                    LevelSearch(level.BlueLine);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                if (level.YellowLine != null)
                {
                    LevelSearch(level.YellowLine);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                if (level.WarpLine != null)
                {
                    LevelSearch(level.WarpLine);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                break;
            case SFLevel.ExitsType.RedYellow:
                if (level.RedLine != null)
                {
                    LevelSearch(level.RedLine);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                if (level.YellowLine != null)
                {
                    LevelSearch(level.YellowLine);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                break;
            case SFLevel.ExitsType.Venom:
                numOfPaths++;
                AddPathItem();
                break;
            default:
                break;
        }
    }

    public void AddPathItem()
    {
        var pathItem = Instantiate(Resources.Load("SFPathControlItem")) as GameObject;
        SFPathControlItem PathItem = pathItem.GetComponent<SFPathControlItem>();
        PathItem.Setup("Path #" + numOfPaths, LevelPath);
        pathItem.transform.SetParent(ScrollView.content, false);
        Paths.Add(PathItem);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var path in Paths)
            {
                path.ShowToggle.isOn = false;
            }

            List<SFPathControlItem> pathsNotCompleted = Paths.Where(p => !p.CompletedToggle.isOn).ToList();

            pathsNotCompleted[Random.Range(0, pathsNotCompleted.Count)].ShowToggle.isOn = true;
        }
    }
}
