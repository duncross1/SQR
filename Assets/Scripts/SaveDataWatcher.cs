using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SaveDataWatcher : MonoBehaviour
{
    [HideInInspector]
    public int levelIndex;
    public bool hasStartedGame;
    public bool hasFinishedGame;
    public int qualityIndex;





    void Awake()
    {
        loadGame(); 
    }
    


    public void setQualityIndex(int qualityIndexIn)
    {
        qualityIndex = qualityIndexIn;
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
        SaveSystem.saveGame(this);
    }

    public void loadGame()
    {

        SaveData data = SaveSystem.loadGame();

        levelIndex = data.levelIndex;
        hasStartedGame = data.hasStartedGame;
        hasFinishedGame = data.hasFinishedGame;
        qualityIndex = data.qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
