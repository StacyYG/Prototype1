using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
	public GameObject player;
	private int giveBirthIndex = 0;
	private int giveBirthIndexLastFrame;
	private int playerNum = 0;
	[SerializeField] int giveBirthInterval = 60;
	[SerializeField] float playerStartSize = 0.2f;
	[SerializeField] float separateTime = 3f;
	public List<GameObject> playerList;

	public CameraController cameraController;

	public ScoreBoard scoreBoard;
	
	
	// Use this for initialization
	void Start ()
	{
		GameObject a = Instantiate(player);
		a.name = "player" + playerNum;
		playerList.Add(a);
		cameraController.player = a.transform;
		scoreBoard.player = a.transform;

	}
	
	// Update is called once per frame
	void Update ()
	{
		GiveBirth();
	}

	private void GiveBirth()
	{
		giveBirthIndex = (int) Time.time / giveBirthInterval;
		if (giveBirthIndex > giveBirthIndexLastFrame || Input.GetKeyDown(KeyCode.B))
		{
			GameObject a = Instantiate(player, playerList[playerList.Count-1].transform.position, 
				playerList[playerList.Count-1].transform.rotation);
			playerNum++;
			a.name = "player" + playerNum;
			a.transform.localScale = new Vector3(1f, 1f, 1f) * playerStartSize;
			playerList.Add(a);
			cameraController.player = a.transform;
			scoreBoard.player = a.transform;
			playerList[playerList.Count-2].GetComponent<Player>().Invoke("Die",separateTime);
			
			
		}

		giveBirthIndexLastFrame = giveBirthIndex;
	}
}
