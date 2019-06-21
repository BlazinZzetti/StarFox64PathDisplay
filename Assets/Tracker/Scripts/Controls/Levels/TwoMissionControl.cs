using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TwoMissionControl : MissionControl
{
    public Image NeutralDarkComplete;
    public Image NeutralHeroComplete;

    public Image PathRouteDarkBoss;
    public Image PathRouteHeroBoss;

    public enum TwoMissionMode
    {
        DarkHeroTop,
        DarkHeroBottom,
        DarkHeroLast,
        DarkNeutral,
        NeutralHero
    }

    public TwoMissionMode Mode = TwoMissionMode.DarkHeroTop;

    void Start ()
    {
        KeyInputField.onEndEdit.AddListener(delegate { UpdateKeys(); });
        switch (Mode)
        {
            case TwoMissionMode.DarkHeroTop:
            case TwoMissionMode.DarkHeroBottom:
            case TwoMissionMode.DarkHeroLast:
                DarkButton.onClick.AddListener(OnDarkButtonPressed);
                HeroButton.onClick.AddListener(OnHeroButtonPressed);
                NeutralButton.gameObject.SetActive(false);
                break;
            case TwoMissionMode.DarkNeutral:
                DarkButton.onClick.AddListener(OnDarkButtonPressed);
                NeutralButton.onClick.AddListener(OnNeutralButtonPressed);
                HeroButton.gameObject.SetActive(false);
                break;
            case TwoMissionMode.NeutralHero:
                NeutralButton.onClick.AddListener(OnNeutralButtonPressed);
                HeroButton.onClick.AddListener(OnHeroButtonPressed);
                DarkButton.gameObject.SetActive(false);
                break;
        }
        
    }

    public void OnNeutralButtonPressed()
    {
        switch(Mode)
        {
            case TwoMissionMode.DarkNeutral:
                NeutralDarkComplete.enabled = !NeutralDarkComplete.enabled;
                break;
            case TwoMissionMode.NeutralHero:
                NeutralHeroComplete.enabled = !NeutralHeroComplete.enabled;
                break;
            default:
                break;
        }
        
        showAllCompleted();
    }

    protected override void showAllCompleted()
    {
        switch(Mode)
        {
            case TwoMissionMode.DarkHeroTop:
            case TwoMissionMode.DarkHeroBottom:
            case TwoMissionMode.DarkHeroLast:
                AllComplete.enabled = (DarkComplete.enabled && HeroComplete.enabled);
                break;
            case TwoMissionMode.DarkNeutral:
                AllComplete.enabled = (DarkComplete.enabled && NeutralDarkComplete.enabled);
                break;
            case TwoMissionMode.NeutralHero:
                AllComplete.enabled = (NeutralHeroComplete.enabled && HeroComplete.enabled);
                break;
            default:
                break;
        }
    }

    public override void ShowNormalPath(bool show)
    {
        switch (Mode)
        {
            case TwoMissionMode.DarkNeutral:
                PathRouteHero.enabled = show;
                break;
            case TwoMissionMode.NeutralHero:
                PathRouteDark.enabled = show;
                break;
            default:
                break;
        }
    }
    public override void ShowDarkPath(bool show)
    {
        switch(Mode)
        {
            case TwoMissionMode.DarkNeutral:
            case TwoMissionMode.DarkHeroTop:
                PathRouteNormal.enabled = show;
                break;
            case TwoMissionMode.DarkHeroBottom:
                PathRouteDark.enabled = show;
                break;
            case TwoMissionMode.DarkHeroLast:
                PathRouteDarkBoss.enabled = show;
                break;
        }
    }
    public override void ShowHeroPath(bool show)
    {
        switch (Mode)
        {         
            case TwoMissionMode.DarkHeroTop:
                PathRouteHero.enabled = show;
                break;
            case TwoMissionMode.NeutralHero:
            case TwoMissionMode.DarkHeroBottom:
                PathRouteNormal.enabled = show;
                break;
            case TwoMissionMode.DarkHeroLast:
                PathRouteHeroBoss.enabled = show;
                break;
        }
    }

    public override string XmlOutput()
    {
        var output = "";
        output += "<MisionControl>\n";
        output += "<DarkComplete>" + DarkComplete.enabled + "</DarkComplete>\n";
        output += "<NeutralDarkComplete>" + NeutralDarkComplete.enabled + "</NeutralDarkComplete>\n";
        output += "<NeutralHeroComplete>" + NeutralHeroComplete.enabled + "</NeutralHeroComplete>\n";
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
        PathRouteDarkBoss.gameObject.SetActive(active);
        PathRouteHeroBoss.gameObject.SetActive(active);
    }

    public void SetAllComplete()
    {
        switch (Mode)
        {
            case TwoMissionMode.DarkHeroTop:
            case TwoMissionMode.DarkHeroBottom:
            case TwoMissionMode.DarkHeroLast:
                DarkComplete.enabled = true;
                HeroComplete.enabled = true;
                break;
            case TwoMissionMode.DarkNeutral:
                DarkComplete.enabled = true;
                NeutralDarkComplete.enabled = true;
                break;
            case TwoMissionMode.NeutralHero:
                NeutralHeroComplete.enabled = true;
                HeroComplete.enabled = true;
                break;
            default:
                break;
        }
        showAllCompleted();
    }

    public override void UpdateRemainingText()
    {
        AllMissionStar.enabled = false;
        DarkMissionStar.enabled = false;
        HeroMissionStar.enabled = false;

        switch (Mode)
        {
            case TwoMissionMode.DarkHeroTop:
            case TwoMissionMode.DarkHeroBottom:
            case TwoMissionMode.DarkHeroLast:
                if (RemainingDarkPlays == 0 && RemainingHeroPlays == 0)
                {
                    AllMissionStar.enabled = true;
                }
                else
                {
                    if (RemainingDarkPlays == 0)
                    {
                        DarkMissionStar.enabled = true;
                    }
                    if (RemainingHeroPlays == 0)
                    {
                        HeroMissionStar.enabled = true;
                    }
                }
                RemainingDarkText.text = RemainingDarkPlays.ToString();
                RemainingHeroText.text = RemainingHeroPlays.ToString();
                break;
            case TwoMissionMode.DarkNeutral:
                if (RemainingDarkPlays == 0 && RemainingNeutralPlays == 0)
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
                        HeroMissionStar.enabled = true;
                    }
                }
                RemainingDarkText.text = RemainingDarkPlays.ToString();
                RemainingHeroText.text = RemainingNeutralPlays.ToString();
                break;
            case TwoMissionMode.NeutralHero:
                if (RemainingNeutralPlays == 0 && RemainingHeroPlays == 0)
                {
                    AllMissionStar.enabled = true;
                }
                else
                {
                    if (RemainingNeutralPlays == 0)
                    {
                        DarkMissionStar.enabled = true;
                    }
                    if (RemainingHeroPlays == 0)
                    {
                        HeroMissionStar.enabled = true;
                    }
                }
                RemainingDarkText.text = RemainingNeutralPlays.ToString();
                RemainingHeroText.text = RemainingHeroPlays.ToString();
                break;
            default:
                break;
        }
    }

    public override void ShowRemainingText(bool show)
    {
        RemainingDarkText.enabled = show;
        RemainingHeroText.enabled = show;
    }
}
