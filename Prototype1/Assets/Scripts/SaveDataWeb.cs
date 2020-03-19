using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SaveDataWeb : MonoBehaviour
{
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

    public static SaveDataWeb Instance;
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
        Record = Services.ScoreBoard.score;

    }

    
}
