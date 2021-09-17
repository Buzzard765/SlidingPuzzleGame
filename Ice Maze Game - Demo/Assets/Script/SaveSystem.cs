using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveGame(LevelSelector levelSelector) {
        try
        {
            BinaryFormatter newFormat = new BinaryFormatter();
            string path = Application.persistentDataPath + "/data.tst";
            FileStream newStream = new FileStream(path, FileMode.Create);

            ProgressData newData = new ProgressData(levelSelector);
            newFormat.Serialize(newStream, newData);
            newStream.Close();
            Debug.Log("saving success");
        } catch
        {
            Debug.Log("saving fail");
        }
    }

    public static ProgressData LoadGame() {
        string path = Application.persistentDataPath + "/data.tst";
        if (File.Exists(path)) {
            BinaryFormatter LoadFormat = new BinaryFormatter();
            FileStream LoadStream = new FileStream(path, FileMode.Open);
            ProgressData LoadData = LoadFormat.Deserialize(LoadStream) as ProgressData;
            LoadStream.Close();
            Debug.Log(path);
            return LoadData;   
        } else {
            Debug.Log("saved File Not Found in" + path);
            return null;
        }
    }
}
