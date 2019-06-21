using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using System.IO;

public class TrackerMenu : MonoBehaviour
{
    public Button LoadFileLocationButton;
    public Button SaveFileButton;
    public Button ReturnToMenuButton;

    public TrackerManager Manager;

    [DllImport("user32.dll")]
    private static extern void OpenFileDialog();

    [DllImport("user32.dll")]
    private static extern void SaveFileDialog();

    // Use this for initialization
    void Start ()
    {
        LoadFileLocationButton.onClick.AddListener(onLoadFileLocationButtonPressed);
        SaveFileButton.onClick.AddListener(onSaveFileButtonPressed);
        ReturnToMenuButton.onClick.AddListener(onReturnToMenuButtonPressed);
	}

    void onLoadFileLocationButtonPressed()
    {
        System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();

        var result = ofd.ShowDialog();

        if (result == System.Windows.Forms.DialogResult.OK && File.Exists(ofd.FileName))
        {
            // Open the file to read from.
            using (StreamReader sr = File.OpenText(ofd.FileName))
            {
                string s = "";
                foreach (var path in Manager.PathControl.Paths)
                {
                    if ((s = sr.ReadLine()) != null)
                    {
                        path.CompletedToggle.isOn = bool.Parse(s);
                    }
                }
            }
            Manager.storedFilePath = ofd.FileName;
        }
    }

    void onSaveFileButtonPressed()
    {
        System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();

        sfd.DefaultExt = ".txt";

        var result = sfd.ShowDialog();

        if (result == System.Windows.Forms.DialogResult.OK)
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(sfd.FileName))
            {
                foreach (var path in Manager.PathControl.Paths)
                {
                    sw.WriteLine(path.CompletedToggle.isOn);
                }
            }
            Manager.storedFilePath = sfd.FileName;
        }
    }

    void onReturnToMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
