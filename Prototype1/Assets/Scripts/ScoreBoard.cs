using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
	public int score;
	public int highScore = -3;
	private Text scoreText;

	// Use this for initialization
	void Start ()
	{
		scoreText = GetComponent<Text>();
	}

	void Update()
	{
		var currentPlayer = Services.Players[Services.Players.Count - 1];
		score = (int)currentPlayer.transform.position.y;
		scoreText.text = "CURRENT HEIGHT:  " + score.ToString();
		if (score > highScore)
		{
			highScore = score;
		}
	}
}
