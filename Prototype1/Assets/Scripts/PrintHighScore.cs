using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintHighScore : MonoBehaviour
{
	private Text highScoreText;

	// Use this for initialization
	void Start ()
	{
		highScoreText = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update ()
	{
		int highScore = GameObject.Find("currentHeight").GetComponent<ScoreBoard>().highScore;
		highScoreText.text = "RECORD:  " + highScore;


	}
}
