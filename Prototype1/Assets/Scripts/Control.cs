using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
	public readonly int giveBirthInterval = 9;
	public readonly float playerStartSize = 0.3f;
	public readonly float separateTime = 3f;
	public GameObject newTree;
	private Player _currentPlayer;
	private GameObject _restartText;
	public bool WaitForReload { get; private set; }

	// Use this for initialization
	private void Awake ()
	{
		Services.Control = this;
		Services.EventManager = new EventManager();
		Services.Players = new List<Player>();
		Services.CameraController = Camera.main.GetComponent<CameraController>();
		Services.Trees = new List<TreeGrowControl>();
		Services.ScoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<ScoreBoard>();
		Services.TreesBound.ResetTreesBound();
		_restartText = GameObject.FindGameObjectWithTag("RestartText");
		_restartText.SetActive(false);
		
		CreateNewPlayer();
	}

	private void Start()
	{
		var t = Instantiate(newTree, new Vector3(-1f, -4f, 0f), Quaternion.identity);
		Services.CompareWithTreesBound(t.transform.position);
		Services.EventManager.Register<NewPlayerBorn>(OnNewPlayerBorn);
	}

	private void CreateNewPlayer()
	{
		var newPlayerObj = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
		newPlayerObj.name = "player " + Services.Players.Count;
		_currentPlayer = newPlayerObj.GetComponent<Player>();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(0);
		}

		if (Services.Players.Count == 0) return;
		if (!_currentPlayer.isAlive)
		{
			WaitForReload = true;
			_restartText.SetActive(true);
		}
	}

	private void OnDestroy()
	{
		Services.EventManager.Unregister<NewPlayerBorn>(OnNewPlayerBorn);
	}

	private void OnNewPlayerBorn(AGPEvent e)
	{
		_currentPlayer = Services.Players.Last();
		
	}
}

