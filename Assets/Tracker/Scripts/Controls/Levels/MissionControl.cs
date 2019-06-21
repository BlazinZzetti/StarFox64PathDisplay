using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public abstract class MissionControl : MonoBehaviour
{
    public Image DarkComplete;
    public Image HeroComplete;
    public Image AllComplete;

    public Image Key1;
    public Image Key2;
    public Image Key3;
    public Image Key4;
    public Image Key5;

    public Button DarkButton;
    public Button NeutralButton;
    public Button HeroButton;

    public int RemainingDarkPlays = 0;
    public int RemainingHeroPlays = 0;
    public int RemainingNeutralPlays = 0;

    public Text RemainingDarkText; 
    public Text RemainingHeroText;
    
    public Image PathRouteNormal;
	public Image PathRouteHero;
	public Image PathRouteDark;

    public Image AllMissionStar;
    public Image HeroMissionStar;
    public Image DarkMissionStar;

    public GameObject KeyEditMode;

    public InputField KeyInputField;
    public GameObject LevelIcon;

    public int NumOfKeys
    {
        get
        {
            if (Key5.enabled)
            {
                return 5;
            }
            if (Key4.enabled)
            {
                return 4;
            }
            if (Key3.enabled)
            {
                return 3;
            }
            if (Key2.enabled)
            {
                return 2;
            }
            if (Key1.enabled)
            {
                return 1;
            }
            return 0;
        }
        set
        {
            if (value < 0 || value > 5)
            {
                Debug.LogError("Invalid Number");
            }
            if (value == 5)
            {
                Key1.enabled = true;
                Key2.enabled = true;
                Key3.enabled = true;
                Key4.enabled = true;
                Key5.enabled = true;
            }
            else if (value == 4)
            {
                Key1.enabled = true;
                Key2.enabled = true;
                Key3.enabled = true;
                Key4.enabled = true;
                Key5.enabled = false;
            }
            else if (value == 3)
            {
                Key1.enabled = true;
                Key2.enabled = true;
                Key3.enabled = true;
                Key4.enabled = false;
                Key5.enabled = false;
            }
            else if (value == 2)
            {
                Key1.enabled = true;
                Key2.enabled = true;
                Key3.enabled = false;
                Key4.enabled = false;
                Key5.enabled = false;
            }
            else if (value == 1)
            {
                Key1.enabled = true;
                Key2.enabled = false;
                Key3.enabled = false;
                Key4.enabled = false;
                Key5.enabled = false;
            }
            else //Value == 0
            {
                Key1.enabled = false;
                Key2.enabled = false;
                Key3.enabled = false;
                Key4.enabled = false;
                Key5.enabled = false;
            }
        }
    }

    protected void UpdateKeys()
    {
        NumOfKeys = int.Parse(KeyInputField.text);
    }

    public void OnDarkButtonPressed()
    {        
        DarkComplete.enabled = !DarkComplete.enabled;
        showAllCompleted();        
    }

    public void OnHeroButtonPressed()
    {
        HeroComplete.enabled = !HeroComplete.enabled;
        showAllCompleted();
    }

    public void ResetRemaining()
    {
        RemainingDarkPlays = 0;
        RemainingHeroPlays = 0;
        RemainingNeutralPlays = 0;
    }

    public abstract string XmlOutput();    

    protected abstract void showAllCompleted();

    public abstract void ShowRemainingText(bool show);
    public abstract void UpdateRemainingText();

    public abstract void ShowNormalPath(bool show);
    public abstract void ShowDarkPath(bool show);
    public abstract void ShowHeroPath(bool show);
    public abstract void SetPathActives(bool active);
}
