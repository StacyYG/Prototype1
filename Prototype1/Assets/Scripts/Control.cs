using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
	public GameObject player;
	public GameObject tree;
	public readonly int giveBirthInterval = 9;
	public readonly float playerStartSize = 0.3f;
	public readonly float separateTime = 3f;
	public GameObject seed;
	public GameObject lastWords;
	public List<GameObject> playerList;

	public CameraController cameraController;

	public ScoreBoard scoreBoard;
	

	
	
	// Use this for initialization
	void Awake ()
	{
		Services.Control = this;
		Services.Players = new List<Player>();
		Services.CameraController = Camera.main.GetComponent<CameraController>();
		
		Instantiate(tree, new Vector3(-1f, -10.5f, 0f), Quaternion.identity);
		CreateNewPlayer();
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
