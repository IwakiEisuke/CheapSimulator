using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class JsonSave : MonoBehaviour
{
    string fileName = "Save.json";
    [SerializeField] CharacterAI character;

    public void Save()
    {
        string saveData = character.SaveStateToJson();
        string filePath = GetFilePath();

        Debug.Log(saveData);

        StreamWriter writer = new(filePath, false);
        writer.Write(saveData);
        writer.Close();
    }

    public void Load()
    {
        string filePath = GetFilePath();
        StreamReader reader = new StreamReader(filePath);
        string json = reader.ReadToEnd();
        reader.Close();

        character.LoadStateFromJson(json);
    }

    private string GetFilePath()
    {
        return Application.dataPath + "/" + fileName;
    }

}
