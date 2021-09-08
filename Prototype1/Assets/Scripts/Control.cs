using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
	#region Prefabs
	
	[Tooltip("Tree prefab")] public GameObject tree;
	[Tooltip("Player prefab")] public GameObject player;
	
	#endregion
	
	private GameObject _endingText;
	public bool GameOver { get; private set; } // if true, wait for game to reload
	
	private void Awake ()
	{
		// Initialize Service Locator
		Services.Control = this;
		Services.EventManager = new EventManager();
		Services.Players = new List<Player>();
		Services.CameraController = Camera.main.GetComponent<CameraController>();
		Services.ScoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<ScoreBoard>();
		Services.treeCount = 0;
		Services.TreeRange.Reset();
		
		// Set up Text GameObject
		_endingText = FindObjectOfType<EndingText>().gameObject;
		_endingText.SetActive(false);
		
		CreateNewPlayer();
	}

	private void Start()
	{
		var firstTree = Instantiate(tree, new Vector3(-1f, -4f, 0f), Quaternion.identity);
		Services.TreeRange.Update(firstTree.transform.position);
	}

	private void CreateNewPlayer()
	{
		var newPlayerObj = Instantiate(player);
		newPlayerObj.name = "player " + Services.Players.Count;
		Services.Players.Add(newPlayerObj.GetComponent<Player>());
	}
	
	void Update ()
	{
		// Press R to reload
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(0);
		}

		if (Input.GetKey(KeyCode.Q))
		{
			ShowConclusion(false);
		}

		if (Input.GetKeyUp(KeyCode.Q))
		{
			if (_endingText.activeSelf)
			{
				_endingText.SetActive(false);
			}
		}
		
		// If current player dies, game over
		if (ReferenceEquals(Services.CurrentPlayer, null)) return;
		if (Services.CurrentPlayer.isAlive) return;
		GameOver = true;
		ShowConclusion(true);
	}

	private void ShowConclusion(bool gameOver)
	{
		if (!_endingText.activeSelf)
		{
			_endingText.SetActive(true);
			_endingText.GetComponent<EndingText>().Print(gameOver);
		}
	}
}

