using UnityEngine;
using System.Collections;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public TrackerManager Manager;

    // Update is called once per frame
    void Update()
    {

    }

    void SaveMissionControls()
    {
        var output = "";
        output += "<Levels>";
        output += Manager.WestopolisControl.XmlOutput();

        output += Manager.DigitalCircuitControl.XmlOutput();
        output += Manager.GlyphicCanyonControl.XmlOutput();
        output += Manager.LethalHighwayControl.XmlOutput();

        output += Manager.CrypticCastleControl.XmlOutput();
        output += Manager.PrisonIslandControl.XmlOutput();
        output += Manager.CircusParkControl.XmlOutput();

        output += Manager.CentralCityControl.XmlOutput();
        output += Manager.TheDoomControl.XmlOutput();
        output += Manager.SkyTroopsControl.XmlOutput();
        output += Manager.MadMatrixControl.XmlOutput();
        output += Manager.DeathRuinsControl.XmlOutput();

        output += Manager.TheARKControl.XmlOutput();
        output += Manager.AirFleetControl.XmlOutput();
        output += Manager.IronJungleControl.XmlOutput();
        output += Manager.SpaceGadgetControl.XmlOutput();
        output += Manager.LostImpactControl.XmlOutput();

        output += Manager.GUNFortressControl.XmlOutput();
        output += Manager.BlackCometControl.XmlOutput();
        output += Manager.LavaShelterControl.XmlOutput();
        output += Manager.CosmicFallControl.XmlOutput();
        output += Manager.FinalHauntControl.XmlOutput();
        output += "</Levels>";
    }
}