using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void GameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Reload()
    {
        Invoke("GameScene", 1f);
    }

    public void GratitudeScene()
    {
        SceneManager.LoadScene("GratitudeScene");
    }

    public void ControlsScene()
    {
        SceneManager.LoadScene("ControlsScene");
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
