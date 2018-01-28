
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : UnitySingletonPersistent<LevelManager> {


    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void QuitRequest()
    {
        Application.Quit();
    }

    public void loadNextLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }
    public void loadLastLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex - 1);
    }
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);

    }

}

