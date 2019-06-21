using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class PathTracker : MonoBehaviour
{
    public Text DisplayText;
    [HideInInspector]
    public int numOfPaths = 0;

    private bool firstPass;

	public PathViewerControl PathControl;

    string filePath;

    void Start()
    {
        filePath = Application.dataPath + "/StreamingAssets/PathSaveFile.txt";
        firstPass = true;
    }

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (PathControl.gameObject.activeSelf)
            {
                SaveData();
            }
            else
            {               
                if (firstPass)
                {
                    StartCoroutine("LoadFileFirstTime");
                    firstPass = false;
                }
                else
                {
                    LoadData();
                }
            }
            PathControl.gameObject.SetActive(!PathControl.gameObject.activeSelf);
        }

        numOfPaths = 0;
        
        foreach (PathControlItem path in PathControl.Paths)
        {
            if (path.CompletedToggle.isOn)
            {
                numOfPaths++;
            }
        }

        DisplayText.text = "Paths: " + numOfPaths + " / 326 " + ((numOfPaths / 326.0f) * 100).ToString("f2") + "%";
    }

    private IEnumerator LoadFileFirstTime()
    {
        while (PathControl.numOfPaths != 326)
        {
            yield return new WaitForSeconds(0.5f);
        }
        LoadData();
    }

    private void SaveData()
    {
        // Create a file to write to.
        using (StreamWriter sw = File.CreateText(filePath))
        {
            foreach (var path in PathControl.Paths)
            {
                sw.WriteLine(path.CompletedToggle.isOn);
            }
        }
    }

    private void LoadData()
    {
        if (File.Exists(filePath))
        {
            // Open the file to read from.
            using (StreamReader sr = File.OpenText(filePath))
            {
                string s = "";
                foreach (var path in PathControl.Paths)
                {
                    if ((s = sr.ReadLine()) != null)
                    {
                        path.CompletedToggle.isOn = bool.Parse(s);
                    }
                }
            }
        }
    }
}
