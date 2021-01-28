using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public SaveDataWatcher sdw;
    public TMP_Dropdown dd;
    
    void Awake()
    {
        dd.value = QualitySettings.GetQualityLevel();
        Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        sdw.setQualityIndex(qualityIndex);
        sdw.SaveGame();
        Debug.Log(QualitySettings.GetQualityLevel());
    }
}
