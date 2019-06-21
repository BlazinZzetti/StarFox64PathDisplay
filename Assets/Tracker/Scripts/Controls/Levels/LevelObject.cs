using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelObject : MonoBehaviour
{
    public RectTransform endPoint;

    public enum LevelMode
    {
        AllMissions,
        DarkHero,
        DarkNeutral,
        NeutralHero
    }

    public Image DarkTwoComplete;
    public Image HeroTwoComplete;
    public Image NeutralDarkComplete;
    public Image NeutralHeroComplete;
    public Image DarkComplete;
    public Image NeutralComplete;
    public Image HeroComplete;

    public Image AllCompleteStar;

    public LevelMode Mode = LevelMode.AllMissions;

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
    public Text RemainingNeutralText;
    public Text RemainingHeroText;

    public Image PathRoute;

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

    // Use this for initialization
    void Start ()
    {
        //KeyInputField.onEndEdit.AddListener(delegate { UpdateKeys(); });
        //DarkButton.onClick.AddListener(OnDarkButtonPressed);
        //NeutralButton.onClick.AddListener(OnNeutralButtonPressed);
        //HeroButton.onClick.AddListener(OnHeroButtonPressed);
    }

    void Update()
    {
        calculatePath();
    }
	
    protected void UpdateKeys()
    {
        NumOfKeys = int.Parse(KeyInputField.text);
    }

    public void OnDarkButtonPressed()
    {
        switch (Mode)
        {
            case LevelMode.AllMissions:
                DarkComplete.enabled = !DarkComplete.enabled;
                break;
            case LevelMode.DarkHero:
                DarkTwoComplete.enabled = !DarkTwoComplete.enabled;
                break;
            case LevelMode.DarkNeutral:
                DarkTwoComplete.enabled = !DarkTwoComplete.enabled;
                break;
            case LevelMode.NeutralHero:
                break;
        }
        showAllCompleted();
    }

    public void OnNeutralButtonPressed()
    {
        //TODO: Set up to work with modes.
        switch (Mode)
        {
            case LevelMode.AllMissions:
                NeutralComplete.enabled = !NeutralComplete.enabled;
                break;
            case LevelMode.DarkHero:
                break;
            case LevelMode.DarkNeutral:
                NeutralDarkComplete.enabled = !NeutralDarkComplete.enabled;
                break;
            case LevelMode.NeutralHero:
                NeutralHeroComplete.enabled = !NeutralHeroComplete.enabled;
                break;
        }
        showAllCompleted();
    }

    public void OnHeroButtonPressed()
    {
        switch (Mode)
        {
            case LevelMode.AllMissions:
                HeroComplete.enabled = !HeroComplete.enabled;
                break;
            case LevelMode.DarkHero:
                HeroTwoComplete.enabled = !HeroTwoComplete.enabled;
                break;
            case LevelMode.DarkNeutral:
                break;
            case LevelMode.NeutralHero:
                HeroTwoComplete.enabled = !HeroTwoComplete.enabled;
                break;
        }
        showAllCompleted();
    }

    public void ResetRemaining()
    {
        RemainingDarkPlays = 0;
        RemainingHeroPlays = 0;
        RemainingNeutralPlays = 0;
    }

    protected void showAllCompleted()
    {
        switch (Mode)
        {
            case LevelMode.AllMissions:
                AllCompleteStar.enabled = (DarkComplete.enabled && NeutralComplete.enabled && HeroComplete.enabled);
                break;
            case LevelMode.DarkHero:
                AllCompleteStar.enabled = (DarkComplete.enabled && HeroComplete.enabled);
                break;
            case LevelMode.DarkNeutral:
                AllCompleteStar.enabled = (DarkComplete.enabled && NeutralDarkComplete.enabled);
                break;
            case LevelMode.NeutralHero:
                AllCompleteStar.enabled = (NeutralHeroComplete.enabled && HeroComplete.enabled);
                break;
        }        
    }

    public void ShowNormalPath(bool show)
    {
        //PathRouteNormal.enabled = show;
    }
    public void ShowDarkPath(bool show)
    {
        //PathRouteDark.enabled = show;
    }
    public void ShowHeroPath(bool show)
    {
        //PathRouteHero.enabled = show;
    }

    public void SetPathActives(bool active)
    {
        //PathRouteDark.gameObject.SetActive(active);
        //PathRouteNormal.gameObject.SetActive(active);
        //PathRouteHero.gameObject.SetActive(active);
    }

    public void ShowRemainingText(bool show)
    {
        //TODO: Set up to work with modes.
        RemainingDarkText.enabled = show;
        RemainingNeutralText.enabled = show;
        RemainingHeroText.enabled = show;
    }

    public void UpdateRemainingText()
    {
        //TODO: Set up to work with modes.
        RemainingDarkText.text = RemainingDarkPlays.ToString();
        RemainingNeutralText.text = RemainingNeutralPlays.ToString();
        RemainingHeroText.text = RemainingHeroPlays.ToString();
    }

    void calculatePath()
    {
        var distance = Vector2.Distance(transform.position, endPoint.position);

        ((RectTransform)transform).sizeDelta.Set(distance, ((RectTransform)transform).sizeDelta.y);

        Vector2 rotation = endPoint.position - transform.position;

        float angleZ = Mathf.Atan2(rotation.y, rotation.x) * (180f / Mathf.PI);

        if (angleZ < 0)
            angleZ += 360;

        PathRoute.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleZ));
        //PathRoute.transform.localScale = new Vector3(length, 1, .2f);
    }
}
