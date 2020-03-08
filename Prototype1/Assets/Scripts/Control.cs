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
	private TreeGrowControl _firstTree;
	
	// Use this for initialization
	private void Awake ()
	{
		Services.Control = this;
		Services.Players = new List<Player>();
		Services.CameraController = Camera.main.GetComponent<CameraController>();
		Services.Trees = new List<TreeGrowControl>();
		
		CreateNewPlayer();
	}

	private void Start()
	{
		var newTree = new GameObject();
		newTree.transform.position = new Vector3(-1f, -10.5f, 0f);
		_firstTree = newTree.AddComponent<TreeGrowControl>();

	}

	private static void CreateNewPlayer()
	{
		GameObject newPlayerObj = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
		newPlayerObj.name = "player " + Services.Players.Count;
		var newPlayer = newPlayerObj.AddComponent<Player>();
		Services.Players.Add(newPlayer);
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(0);
		}

		if (Services.Players.Count == 0) return;
		if (!Services.Players.Last().isAlive)
		{
			StartCoroutine(WaitAndReloadLevel(2f));
		}
	}
	

	private IEnumerator WaitAndReloadLevel(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		SceneManager.LoadScene(0);
	}
}
