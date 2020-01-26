using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FileReadWrite
{
    public static void WriteToJsonFile<T>(T objectToWrite)
    {
        string data = JsonUtility.ToJson(objectToWrite);
        SaveSystem.Save(data);

        Debug.Log("File was written to json.");
    }

    public static T ReadFromJsonFile<T>()
    {
        string saveString = SaveSystem.Load();

        if (saveString != null)
        {
            var savedObject = JsonUtility.FromJson<T>(saveString);

            Debug.Log("File was loaded from file.");

            return (T)savedObject;
        }

        return default(T);
    }
}
