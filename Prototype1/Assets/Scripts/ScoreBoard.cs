using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
	public int score;
	public int highScore;
	private Text scoreText;

	// Use this for initialization
	void Start ()
	{
		scoreText = GetComponent<Text>();
	}

	void Update()
	{
		if (Services.Players.Count == 0) return;
		scoreText.text = "CURRENT HEIGHT:	" + score + "\nRECORD:	" + GetHighScore();
		
	}

	public int GetHighScore()
	{
		var currentPlayer = Services.Players.Last();
		score = (int) currentPlayer.transform.position.y;
		if (score >= highScore)
		{
			highScore = score;
		}

		return highScore;
	}
}
