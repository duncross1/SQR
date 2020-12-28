using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public LevelLoader levelLoader;
    public GameObject playMenu;
    public GameObject startedGameMenu;
    public GameObject finishedGameMenu;
    public SaveDataWatcher saveDataWatcher;


    public void PlayButton()
    {
        if(saveDataWatcher.hasStartedGame == true)
        {
            if (saveDataWatcher.hasFinishedGame == true)
            {
                finishedGameMenu.SetActive(true);
            }
            else
            {
                startedGameMenu.SetActive(true);
            }
        }
        else
        {
            playMenu.SetActive(true);
        }
        

       
    }

    public void newGameButton()
    {
        Destroy(GameObject.Find("MenuMusicSource"));
        GameObject.Find("GameMusicSource").gameObject.GetComponent<AudioSource>().Play();
        levelLoader.loadNextLevel();
    }

    public void continueButton()
    {
        Destroy(GameObject.Find("MenuMusicSource"));
        GameObject.Find("GameMusicSource").gameObject.GetComponent<AudioSource>().Play();
        levelLoader.loadGivenLevel(saveDataWatcher.levelIndex);
    }

    public void levelSelectButton()
    {
        Destroy(GameObject.Find("MenuMusicSource"));
        GameObject.Find("GameMusicSource").gameObject.GetComponent<AudioSource>().Play();
        levelLoader.loadGivenLevel(47);
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Exit (application.quit() doesnt work in-editor but will when built");
    }
}
