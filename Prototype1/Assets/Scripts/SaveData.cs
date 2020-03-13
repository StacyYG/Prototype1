using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class SaveData : MonoBehaviour
{
    public const string FileName = "/DataFile.txt";
    private int _record;
    public int Record
    {
        get => _record;
        set
        {
            if (_record < value)
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
        Load();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Record = Services.ScoreBoard.score;
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
            saveRecord = Record,
            savePlayer = new PlayerInfo()
        };
        foreach (var player in Services.Players)
        {
            saveObject.savePlayer.position = player.transform.position;
            saveObject.savePlayer.rotation = player.transform.rotation;
            saveObject.savePlayer.scale = player.transform.localScale;
            saveObject.savePlayer.hasSeed = player.hasSeed;
            saveObject.savePlayer.isAlive = player.isAlive;
            saveObject.savePlayer.hasLastWords = player.hasLastWords;
        }
        var json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + FileName,json);
        Debug.Log("saved");
        
    }

    private void Load()
    {
        if (!File.Exists(Application.dataPath + FileName)) return;
        var saveString = File.ReadAllText(Application.dataPath + FileName);
        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
        
        Instance._record = saveObject.saveRecord;
        
        Debug.Log("loaded");
    }

    public struct PlayerInfo
    {
        public Vector3 position, scale;
        public Quaternion rotation;
        public bool isAlive, hasSeed, hasLastWords;
    }
    private class SaveObject
    {
        public int saveRecord;
        public PlayerInfo savePlayer;
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    
}
