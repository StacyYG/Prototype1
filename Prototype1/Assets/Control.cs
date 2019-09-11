using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
	public GameObject player;

	private int playerNum = 0;
	[SerializeField] int giveBirthInterval = 9;
	[SerializeField] float playerStartSize = 0.2f;
	[SerializeField] float separateTime = 3f;
	public List<GameObject> playerList;

	public CameraController cameraController;

	public ScoreBoard scoreBoard;

	//public CollectFlower collectFlower;
	
	int currentSceneIndex = 0;

	
	
	// Use this for initialization
	void Start ()
	{
		GameObject a = Instantiate(player);
		a.name = "player" + playerNum;
		playerList.Add(a);
		cameraController.player = a.transform;
		scoreBoard.player = a.transform;
		InvokeRepeating("GiveBirth",giveBirthInterval,giveBirthInterval);


	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			ReloadLevel();
		}

		if (!playerList[playerList.Count-1].GetComponent<Player>().isAlive)
		{
			Invoke("ReloadLevel",2f);
		}
	}

	private void GiveBirth()
	{
		GameObject a = Instantiate(player, playerList[playerList.Count-1].transform.position, 
			playerList[playerList.Count-1].transform.rotation);
		playerNum++;
		a.name = "player" + playerNum;
		a.transform.localScale = new Vector3(1f, 1f, 1f) * playerStartSize;
		playerList.Add(a);
		cameraController.player = a.transform;
		scoreBoard.player = a.transform;
		//collectFlower.player = a;
		playerList[playerList.Count - 2].GetComponent<Player>().hasSeed = false;
		playerList[playerList.Count - 2].GetComponent<Player>().Invoke("Die", separateTime);

	}
	
	void ReloadLevel()
	{
		SceneManager.LoadScene(currentSceneIndex);
	}
}
