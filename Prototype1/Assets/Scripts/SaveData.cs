using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    public const string fileName = "/DataFile.txt";
    private int _record;

    public int Record
    {
        get => _record;
        set
        {
            if (value > _record)
            {
                _record = value;
            }
        }
    }

    public static SaveData Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Record = Services.ScoreBoard.highScore;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Load();
        }
    }

    private void Save()
    {
        SaveObject saveObject = new SaveObject
        {
            savedRecord = _record,
        };
        var json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + fileName,json);
        Debug.Log("saved");
    }

    private void Load()
    {
        if (!File.Exists(Application.dataPath + fileName)) return;
        var saveString = File.ReadAllText(Application.dataPath + fileName);
        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
        Instance.Record = saveObject.savedRecord;
        Debug.Log("logged");
    }

    private class SaveObject
    {
        public int savedRecord;
    }
    
}
