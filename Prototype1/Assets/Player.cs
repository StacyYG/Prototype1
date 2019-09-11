using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	Rigidbody2D rigidBody;
	
	[SerializeField] int maxJumpTimes = 2;
	[SerializeField] float jumpForce = 400f;
	[SerializeField] float horizontalMultiplier = 200f;
	[SerializeField] float maxSpeedX = 12f;
	
	[SerializeField] float playerGrowMultiplier = 1f;
	[SerializeField] GameObject seed;
	private SpriteRenderer spriteRenderer;

	
	private float velocityLastFrame;

	public bool isAlive = true;

	
	int jumpTimes = 0;
	
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

		float movementThisFrame = horizontalMultiplier * Time.deltaTime;
		float velocityY = rigidBody.velocity.y;
		
		if (Input.GetKey(KeyCode.A))
		{
			if (velocityLastFrame == -1f)
			{
				rigidBody.AddForce(Vector2.left * movementThisFrame);
			}
			else
			{
				rigidBody.velocity = new Vector2(0,velocityY);
				rigidBody.AddForce(Vector2.left * movementThisFrame);
			}
			
			velocityLastFrame = -1f;
		}

		if (Input.GetKey(KeyCode.D))
		{
			if (velocityLastFrame == 1f)
			{
				rigidBody.AddForce(Vector2.right * movementThisFrame);
			}
			else
			{
				rigidBody.velocity = new Vector2(0,velocityY);
				rigidBody.AddForce(Vector2.right * movementThisFrame);
			}
			
			velocityLastFrame = 1f;
		}
		
		float velocityX = rigidBody.velocity.x;
		float clampedVelocityX;
		clampedVelocityX = Mathf.Clamp(velocityX, -maxSpeedX, maxSpeedX);
		rigidBody.velocity = new Vector2(clampedVelocityX,velocityY);
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!isAlive)
		{
			return;
		}

		switch (collision.gameObject.tag)
		{
			case "unfriendly":
				Die();
				break;
			case "branch":
				jumpTimes = 0;
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
		if (Input.GetKeyDown(KeyCode.S))
		{
			List<GameObject> playerList = GameObject.Find("Control").GetComponent<Control>().playerList;
			
			GameObject a = Instantiate(seed, playerList[playerList.Count-1].transform.position, Quaternion.identity);
			
			
		}
	}
	

}
