using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
	public int score;
	public int highScore = -3;
	private Text scoreText;
	[SerializeField] public Transform player;

	// Use this for initialization
	void Start ()
	{

		scoreText = GetComponent<Text>();


	}

	void Update()
	{
		score = (int)player.transform.position.y;
		scoreText.text = "CURRENT HEIGHT:  " + score.ToString();
		if (score > highScore)
		{
			highScore = score;
		}
	}
}
