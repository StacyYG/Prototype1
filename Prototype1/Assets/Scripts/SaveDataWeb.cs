using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SaveDataWeb : MonoBehaviour
{
    public int Record { get; private set; }

    public static SaveDataWeb instance;
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
    }
    
    private void Update()
    {
        if (Record < Services.ScoreBoard.Score)
        {
            Record = Services.ScoreBoard.Score;
        }
    }
}
