using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D rigidBody;
	
	[SerializeField] private int maxJumpTimes = 2;
	[SerializeField] private float jumpForce = 400f;
	[SerializeField] private float horizontalMultiplier = 200f;
	[SerializeField] private float maxSpeedX = 12f;
	
	[SerializeField] private float playerGrowMultiplier = 1f;
	[SerializeField] private GameObject seed;
	[SerializeField] private GameObject lastWords;
	private bool hasLastWords = true;
	private SpriteRenderer spriteRenderer;

	
	private int velocityLastFrame;

	public bool isAlive = true;

	public bool hasSeed = true;

	
	private int jumpTimes;
	
	// Use this for initialization
	void Start ()
	{

		spriteRenderer = GetComponent<SpriteRenderer>();
		rigidBody = GetComponent<Rigidbody2D>();
		

	}
	
	// Update is called once per frame
	void Update () {
		if (isAlive)
		{
			RespondToMovement();
			RespondToPlantSeed();
		}


		if (gameObject.transform.localScale.x < 1)
		{
			gameObject.transform.localScale += new Vector3(1f,1f,1f) * playerGrowMultiplier * Time.deltaTime;
		}

		

	}


	private void RespondToMovement()
	{
		
		if (Input.GetKeyDown(KeyCode.Space) && jumpTimes < maxJumpTimes)
		{
			rigidBody.AddForce(Vector2.up * jumpForce);
			jumpTimes++;
		}

		float forceThisFrame = horizontalMultiplier * Time.deltaTime;
		float velocityY = rigidBody.velocity.y;
		
		if (Input.GetKey(KeyCode.A))
		{
			if (velocityLastFrame == -1)
			{
				rigidBody.AddForce(Vector2.left * forceThisFrame);
			}
			else
			{
				rigidBody.velocity = new Vector2(0,velocityY);
				rigidBody.AddForce(Vector2.left * forceThisFrame);
			}
			
			velocityLastFrame = -1;
		}

		if (Input.GetKey(KeyCode.D))
		{
			if (velocityLastFrame == 1)
			{
				rigidBody.AddForce(Vector2.right * forceThisFrame);
			}
			else
			{
				rigidBody.velocity = new Vector2(0,velocityY);
				rigidBody.AddForce(Vector2.right * forceThisFrame);
			}
			
			velocityLastFrame = 1;
		}
		
		float velocityX = rigidBody.velocity.x;
		float clampedVelocityX;
		clampedVelocityX = Mathf.Clamp(velocityX, -maxSpeedX, maxSpeedX);
		rigidBody.velocity = new Vector2(clampedVelocityX,velocityY);
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{

		switch (collision.gameObject.tag)
		{
			case "unfriendly":
				Die();
				break;
			case "branch":
				jumpTimes = 0;
				break;
			case "Player":
				jumpTimes = 0;
				if ((!isAlive) && hasLastWords)
				{
					Instantiate(lastWords, transform.position + new Vector3(0,2,0), Quaternion.identity);
					hasLastWords = false;
				}

				break;
			
		}

		
	}

	public void Die()
	{
		isAlive = false;
		spriteRenderer.color = new Color(1f,1f,1f,0.5f);
		
	}

	void RespondToPlantSeed()
	{
		
		if (Input.GetKeyDown(KeyCode.S) && hasSeed)
		{
			List<GameObject> playerList = GameObject.Find("Control").GetComponent<Control>().playerList;
				
			Instantiate(seed, playerList[playerList.Count-1].transform.position, Quaternion.identity);
			
				

			hasSeed = false;
		}
		
	}
	

}
