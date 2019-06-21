using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class PathViewerControl : MonoBehaviour 
{
	public ScrollRect ScrollView;

	public List<PathControlItem> Paths = new List<PathControlItem> ();

    public List<Level> LevelPath = new List<Level>();

    public InputField SearchTextField;
    public Button SearchButton;

    //Used to determine when the path control items are fully loaded.
    [HideInInspector]
    public int numOfPaths = 0;

    public TrackerManager Manager;

    // Use this for initialization
    void Start () 
	{
        SearchButton.onClick.AddListener(onSearchButtonPressed);

        numOfPaths = 0;

        Level SonicDiablonPD    = new Level(null, Level.LevelMode.FinalLevel, null, null, null);
        Level BlackDoomPD       = new Level(null, Level.LevelMode.FinalLevel, null, null, null);
        Level SonicDiablonD     = new Level(null, Level.LevelMode.FinalLevel, null, null, null);

        Level EggDealerD        = new Level(null, Level.LevelMode.FinalLevel, null, null, null);
        Level EggDealerND       = new Level(null, Level.LevelMode.FinalLevel, null, null, null);
        Level EggDealerNH       = new Level(null, Level.LevelMode.FinalLevel, null, null, null);
        Level EggDealerH        = new Level(null, Level.LevelMode.FinalLevel, null, null, null);

        Level BlackDoomH        = new Level(null, Level.LevelMode.FinalLevel, null, null, null);
        Level SonicDiablonPH    = new Level(null, Level.LevelMode.FinalLevel, null, null, null);
        Level BlackDoomPH       = new Level(null, Level.LevelMode.FinalLevel, null, null, null);

        Level GUNFortress       = new Level(Manager.GUNFortressControl, Level.LevelMode.HeroDark, SonicDiablonPD, null, BlackDoomPD);
        Level BlackComet        = new Level(Manager.BlackCometControl, Level.LevelMode.HeroDark, SonicDiablonD, null, EggDealerD);
        Level LavaShelter       = new Level(Manager.LavaShelterControl, Level.LevelMode.HeroDark, EggDealerND, null, EggDealerNH);
        Level CosmicFall        = new Level(Manager.CosmicFallControl, Level.LevelMode.HeroDark, BlackDoomH, null, EggDealerH);
        Level FinalHaunt        = new Level(Manager.FinalHauntControl, Level.LevelMode.HeroDark, SonicDiablonPH, null, BlackDoomPH);
                                                         
        Level TheARK            = new Level(Manager.TheARKControl, Level.LevelMode.DarkNeutral, GUNFortress, BlackComet, null);
        Level AirFleet          = new Level(Manager.AirFleetControl, Level.LevelMode.AllMissions, GUNFortress, BlackComet, LavaShelter);
        Level IronJungle        = new Level(Manager.IronJungleControl, Level.LevelMode.AllMissions, BlackComet, LavaShelter, CosmicFall);
        Level SpaceGadget       = new Level(Manager.SpaceGadgetControl, Level.LevelMode.AllMissions, LavaShelter, CosmicFall, FinalHaunt);
        Level LostImpact        = new Level(Manager.LostImpactControl, Level.LevelMode.HeroNeutral, null, CosmicFall, FinalHaunt);
                                                         
        Level CentralCity       = new Level(Manager.CentralCityControl, Level.LevelMode.HeroDark, TheARK, null, AirFleet);
        Level TheDoom           = new Level(Manager.TheDoomControl, Level.LevelMode.AllMissions, TheARK, AirFleet, IronJungle);
        Level SkyTroops         = new Level(Manager.SkyTroopsControl, Level.LevelMode.AllMissions, AirFleet, IronJungle, SpaceGadget);
        Level MadMatrix         = new Level(Manager.MadMatrixControl, Level.LevelMode.AllMissions, IronJungle, SpaceGadget, LostImpact);
        Level DeathRuins        = new Level(Manager.DeathRuinsControl, Level.LevelMode.HeroDark, SpaceGadget, null, LostImpact);
                                                         
        Level CrypticCastle     = new Level(Manager.CrypticCastleControl, Level.LevelMode.AllMissions, CentralCity, TheDoom, SkyTroops);
        Level PrisonIsland      = new Level(Manager.PrisonIslandControl, Level.LevelMode.AllMissions, TheDoom, SkyTroops, MadMatrix);
        Level CircusPark        = new Level(Manager.CircusParkControl, Level.LevelMode.AllMissions, SkyTroops, MadMatrix, DeathRuins);
                                                         
        Level DigitalCircuit    = new Level(Manager.DigitalCircuitControl, Level.LevelMode.HeroDark, CrypticCastle, null, PrisonIsland);
        Level GlyphicCanyon     = new Level(Manager.GlyphicCanyonControl, Level.LevelMode.AllMissions, CrypticCastle, PrisonIsland, CircusPark);
        Level LethalHighway     = new Level(Manager.LethalHighwayControl, Level.LevelMode.HeroDark, PrisonIsland, null, CircusPark);
                                                         
        Level Westopolis        = new Level(Manager.WestopolisControl, Level.LevelMode.AllMissions, DigitalCircuit, GlyphicCanyon, LethalHighway);

        LevelSearch(Westopolis);
    }

    public void LevelSearch(Level level)
    {
        LevelPath.Add(level);
        switch (level.levelMode)
        {
            case Level.LevelMode.AllMissions:
                if (level.NextLevelDark != null)
                {
                    LevelSearch(level.NextLevelDark);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                if (level.NextLevelNeutral != null)
                {
                    LevelSearch(level.NextLevelNeutral);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                if (level.NextLevelHero != null)
                {
                    LevelSearch(level.NextLevelHero);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                break;
            case Level.LevelMode.DarkNeutral:
                if (level.NextLevelDark != null)
                {
                    LevelSearch(level.NextLevelDark);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                if (level.NextLevelNeutral != null)
                {
                    LevelSearch(level.NextLevelNeutral);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                break;
            case Level.LevelMode.HeroDark:
                if (level.NextLevelDark != null)
                {
                    LevelSearch(level.NextLevelDark);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                if (level.NextLevelHero != null)
                {
                    LevelSearch(level.NextLevelHero);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                break;
            case Level.LevelMode.HeroNeutral:
                if (level.NextLevelNeutral != null)
                {
                    LevelSearch(level.NextLevelNeutral);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                if (level.NextLevelHero != null)
                {
                    LevelSearch(level.NextLevelHero);
                    LevelPath.Remove(LevelPath[LevelPath.Count - 1]);
                }
                else
                {
                    numOfPaths++;
                    AddPathItem();
                }
                break;
            case Level.LevelMode.FinalLevel:
                numOfPaths++;
                AddPathItem();
                break;
            default:
                break;

        }
    }

    public void AddPathItem()
    {
        var pathItem = Instantiate(Resources.Load("PathControlItem")) as GameObject;
        PathControlItem PathItem = pathItem.GetComponent<PathControlItem>();
        PathItem.Setup("Path #" + numOfPaths, LevelPath);
        pathItem.transform.SetParent(ScrollView.content, false);
        Paths.Add(PathItem);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Generate Random number between 1 and 326
            //Paths[Random.Range(0, 326)].ShowToggle.isOn = true;

            foreach (var path in Paths)
            {
                path.ShowToggle.isOn = false;
            }

            List<PathControlItem> pathsNotCompleted = Paths.Where(p => !p.CompletedToggle.isOn).ToList();

            pathsNotCompleted[Random.Range(0, pathsNotCompleted.Count)].ShowToggle.isOn = true;
        }
    }

    void onSearchButtonPressed()
    {
        var searchText = SearchTextField.text;
        int searchInt;

        if (int.TryParse(searchText, out searchInt))
        {           
            if (searchInt >= 1 && searchInt <= 326)
            {
                //Determine if Text is used for path number or code.
                foreach (var path in Paths)
                {
                    path.ShowToggle.isOn = false;
                }
                Paths[searchInt - 1].ShowToggle.isOn = true;
            }
        }
        else if (searchText.Length == 6)
        {
            //Detect if only H, N, and D are in the string.
            int hCount = 0;
            int nCount = 0;
            int dCount = 0;

            foreach (var letter in searchText)
            {
                if (letter == 'H')
                {
                    hCount++;
                }
                if (letter == 'N')
                {
                    nCount++;
                }
                if (letter == 'D')
                {
                    dCount++;
                }
            }
            if ((hCount + nCount + dCount) == 6)
            {
                var foundPath = Paths.Find(p => p.PathCode == searchText);
                if (foundPath != null)
                {
                    foreach (var path in Paths)
                    {
                        path.ShowToggle.isOn = false;
                    }
                    foundPath.ShowToggle.isOn = true;
                }
            }
        }
        SearchTextField.text = "";
    }
}
