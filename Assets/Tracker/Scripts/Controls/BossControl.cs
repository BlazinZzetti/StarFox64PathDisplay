using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossControl : MonoBehaviour
{
    public Image BossComplete;

    public Button BossButton;

    // Use this for initialization
    void Start ()
    {
        BossButton.onClick.AddListener(OnBossButtonPressed);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnBossButtonPressed()
    {
        BossComplete.enabled = !BossComplete.enabled;
    }
}
