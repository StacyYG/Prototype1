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
                File.WriteAllText(Application.dataPath + fileName,"Record," + _record);
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

        string dataFileText = File.ReadAllText(Application.dataPath + fileName);
        if (dataFileText == "") return;
        string[] textSplit = dataFileText.Split(',');
        Record = int.Parse(textSplit[1]);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Record = Services.ScoreBoard.highScore;
    }
    
    
}
