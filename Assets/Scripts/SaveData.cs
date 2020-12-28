using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{

    public int levelIndex;
    public bool hasStartedGame;
    public bool hasFinishedGame;
    
    public SaveData(SaveDataWatcher sdw)
    {
        levelIndex = sdw.levelIndex;
        hasStartedGame = sdw.hasStartedGame;
        hasFinishedGame = sdw.hasFinishedGame;

    }
}
