using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
 
    public static void saveGame(SaveDataWatcher sdw)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Save.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(sdw);

        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData loadGame()
    {
        string path = Application.persistentDataPath + "/Save.bin";
        if(File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = binaryFormatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File Not Found");
            return null;
        }
    }
}
