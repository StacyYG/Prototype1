using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class SaveData : MonoBehaviour
{
    public const string FileName = "/DataFile.txt";
    public int Record { get; private set; }

    public static SaveData instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        Load();
    }

    void Update()
    {
        if (Record < Services.ScoreBoard.Score)
        {
            Record = Services.ScoreBoard.Score;
        }
    }

    private void Save()
    {
        SaveObject saveObject = new SaveObject
        {
            saveRecord = Record,
        };
        
        var json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + FileName,json);
    }

    private void Load()
    {
        if (!File.Exists(Application.dataPath + FileName)) return;
        var saveString = File.ReadAllText(Application.dataPath + FileName);
        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
        
        instance.Record = saveObject.saveRecord;
    }
    
    private class SaveObject
    {
        public int saveRecord;
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
