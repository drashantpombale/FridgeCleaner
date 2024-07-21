using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoSingletonGeneric<SceneLoader>
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void LoadGameScene() 
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void MainMenuScene() 
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
