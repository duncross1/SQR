using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public LevelLoader levelLoader;

    public void PlayButton()
    {
        FindObjectOfType<AudioManager>().PlaySong("Music");
        levelLoader.loadNextLevel();
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Exit (application.quit() doesnt work in-editor but will when built");
    }
}
