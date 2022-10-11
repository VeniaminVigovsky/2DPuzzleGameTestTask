using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class SaveFileSerializationController
{
    private static string PATH = Application.persistentDataPath + "/sd.json";

    public static void SerializeSaveData(SaveData saveData)
    {
        var jsonString = JsonUtility.ToJson(saveData);

        File.WriteAllText(PATH, jsonString);
    }

    public static SaveData DeserializeSaveData()
    {
        SaveData saveData;

        if (!File.Exists(PATH))
        {
            saveData = new SaveData(new List<int>(), new List<int>(), 0.5f);
        }
        else
        {
            var json = File.ReadAllText(PATH);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        return saveData;
    }
}
