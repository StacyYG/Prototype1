using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
	public int currentHeight;
	public int score;
	private Text scoreText;
	private Transform _currentPlayer;

	// Use this for initialization
	private void Start ()
	{
		scoreText = GetComponent<Text>();
		Services.EventManager.Register<NewPlayerBorn>(OnNewPlayerBorn);
		_currentPlayer = Services.Players.Last().transform;
	}

	private void OnDestroy()
	{
		Services.EventManager.Unregister<NewPlayerBorn>(OnNewPlayerBorn);
	}

	private void Update()
	{
		if (Services.Players.Count == 0) return;
		SetScores();
		scoreText.text = "CURRENT HEIGHT:\t" + currentHeight + "\nSCORE:\t" + score + "\nRECORD:\t" + SaveData.Instance.Record;
		
	}

	public void SetScores()
	{
		currentHeight = (int) _currentPlayer.position.y;
		if (score < currentHeight)
		{
			score = currentHeight;
		}
	}

	private void OnNewPlayerBorn(AGPEvent e)
	{
		_currentPlayer = Services.Players.Last().transform;
	}
}
