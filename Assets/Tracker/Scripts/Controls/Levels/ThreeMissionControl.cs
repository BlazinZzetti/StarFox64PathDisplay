using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ThreeMissionControl : MissionControl
{
    public Image NeutralComplete;

    public Image NeutralMissionStar;

    public Text RemainingNeutralText;

    void Start ()
    {
        KeyInputField.onEndEdit.AddListener(delegate { UpdateKeys(); });
        DarkButton.onClick.AddListener(OnDarkButtonPressed);
        NeutralButton.onClick.AddListener(OnNeutralButtonPressed);
        HeroButton.onClick.AddListener(OnHeroButtonPressed);
    }

    public void OnNeutralButtonPressed()
    {
        NeutralComplete.enabled = !NeutralComplete.enabled;
        showAllCompleted();
    }

    protected override void showAllCompleted()
    {
        AllComplete.enabled = (DarkComplete.enabled && NeutralComplete.enabled && HeroComplete.enabled);
    }

    public override void ShowNormalPath(bool show)
    {
        PathRouteNormal.enabled = show;
    }
    public override void ShowDarkPath(bool show)
    {
        PathRouteDark.enabled = show;
    }
    public override void ShowHeroPath(bool show)
    {
        PathRouteHero.enabled = show;
    }

    public override string XmlOutput()
    {
        var output = "";
        output += "<MisionControl>\n";
        output += "<DarkComplete>" + DarkComplete.enabled + "</DarkComplete>\n";
        output += "<NeutralComplete>" + NeutralComplete.enabled + "</NeutralComplete>\n";
        output += "<HeroComplete>" + HeroComplete.enabled + "</HeroComplete>\n";
        output += "<NumOfKeys>" + NumOfKeys + "</NumOfKeys>\n";
        output += "</MisionControl>\n";
        return output;
    }

    public override void SetPathActives(bool active)
    {
        PathRouteDark.gameObject.SetActive(active);
        PathRouteNormal.gameObject.SetActive(active);
        PathRouteHero.gameObject.SetActive(active);
    }

    public void SetAllComplete()
    {
        DarkComplete.enabled = true;
        NeutralComplete.enabled = true;
        HeroComplete.enabled = true;
        showAllCompleted();
    }

    public override void ShowRemainingText(bool show)
    {
        RemainingDarkText.enabled = show;
        RemainingNeutralText.enabled = show;
        RemainingHeroText.enabled = show;
    }

    public override void UpdateRemainingText()
    {
        AllMissionStar.enabled = false;
        DarkMissionStar.enabled = false;
        NeutralMissionStar.enabled = false;
        HeroMissionStar.enabled = false;

        if (RemainingDarkPlays == 0 && RemainingNeutralPlays == 0 && RemainingHeroPlays == 0)
        {
            AllMissionStar.enabled = true;
        }
        else
        {
            if (RemainingDarkPlays == 0)
            {
                DarkMissionStar.enabled = true;
            }
            if (RemainingNeutralPlays == 0)
            {
                NeutralMissionStar.enabled = true;
            }
            if (RemainingHeroPlays == 0)
            {
                HeroMissionStar.enabled = true;
            }
        }

        RemainingDarkText.text = RemainingDarkPlays.ToString();
        RemainingNeutralText.text = RemainingNeutralPlays.ToString();
        RemainingHeroText.text = RemainingHeroPlays.ToString();       
    }   
}
