using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataWatcher : MonoBehaviour
{
    [HideInInspector]
    public int levelIndex;
    public bool hasStartedGame;
    public bool hasFinishedGame;




    void Awake()
    {
        loadGame();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            loadGame();
        }
    }

    public void SaveGame()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            hasStartedGame = true;
        }
        if (SceneManager.GetActiveScene().buildIndex == 43)
        {
            hasFinishedGame = true;
        }
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("ts: " + levelIndex);
        SaveSystem.saveGame(this);
    }

    public void loadGame()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);

        SaveData data = SaveSystem.loadGame();

        levelIndex = data.levelIndex;
        hasStartedGame = data.hasStartedGame;
        hasFinishedGame = data.hasFinishedGame;

        Debug.Log(levelIndex);
        Debug.Log("startedGame: " + hasStartedGame);
        Debug.Log("finishedGame " + hasFinishedGame);
    }
}
