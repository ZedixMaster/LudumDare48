using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public SettingsMenu settings;

    private void Start()
    {
        settings.LoadSettings();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Lake");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
