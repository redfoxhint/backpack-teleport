using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class FileReadWrite
{
    public static void WriteToJsonFile<T>(T objectToWrite)
    {
        string data = JsonUtility.ToJson(objectToWrite);

        string encryptedData = EncryptDecryptJson(data, 200);

        SaveSystem.Save(encryptedData);

        Debug.Log("File was written to json.");
    }

    public static T ReadFromJsonFile<T>()
    {
        string saveString = SaveSystem.Load();

        if (saveString != null)
        {
            string decryptedKey = EncryptDecryptJson(saveString, 200);
            var savedObject = JsonUtility.FromJson<T>(decryptedKey);

            Debug.Log("File was loaded from file.");

            return (T)savedObject;
        }

        return default(T);
    }

    private static string EncryptDecryptJson(string data, int encryptionKey)
    {
        StringBuilder inputSB = new StringBuilder(data);
        StringBuilder outputSB = new StringBuilder(data.Length);

        char textCH;

        for (int i = 0; i < data.Length; i++)
        {
            textCH = inputSB[i];
            textCH = (char)(textCH ^ encryptionKey);
            outputSB.Append(textCH);
        }

        return outputSB.ToString();
    }
}
