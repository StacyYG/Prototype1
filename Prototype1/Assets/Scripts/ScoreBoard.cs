using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
	[SerializeField] private bool isWeb = true;
	public int Score { get; private set; }
	private int _currentHeight;
	private TextMeshProUGUI _scoreTMP;

	private void Start ()
	{
		_scoreTMP = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		if (ReferenceEquals(Services.CurrentPlayer, null)) return;
		
		SetScores(); 
		// current height: current player Y
		// score: largest height this play
		// record: highest score across plays 
		
		if (isWeb) // played on browser
		{
			_scoreTMP.text = $"Current height: {_currentHeight}\nScore: {Score}\nRecord: {SaveDataWeb.instance.Record}";
		}
		else // local save file
		{
			_scoreTMP.text = $"Current height: {_currentHeight}\nScore: {Score}\nRecord: {SaveData.instance.Record}";
		}
	}

	private void SetScores()
	{
		_currentHeight = (int) Services.CurrentPlayer.transform.position.y;
		if (Score < _currentHeight)
		{
			Score = _currentHeight;
		}
	}
}
