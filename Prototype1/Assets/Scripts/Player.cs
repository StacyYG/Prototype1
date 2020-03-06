using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D _rigidBody;
	private SpriteRenderer _spriteRenderer;
	private int _jumpTimes;
	private int maxJumpTimes = 2;
	private float jumpForce = 800f;
	private float horizontalMultiplier = 4000f;
	private float maxSpeedX = 12f;
	private float playerGrowMultiplier = 0.1f;
	public bool isAlive = true;
	private bool hasSeed = true;
	private bool hasLastWords = true;


	private int velocityLastFrame;

	public GameObject PlayerGameObject { get; private set; }

	private void Start()
	{
		_rigidBody = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		Invoke("GiveBirth", Services.Control.giveBirthInterval);
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
			gameObject.transform.localScale += playerGrowMultiplier * Time.deltaTime * Vector3.one;
		}
		
	}


	private void RespondToMovement()
	{
		
		if (Input.GetKeyDown(KeyCode.Space) && _jumpTimes < maxJumpTimes)
		{
			_rigidBody.AddForce(Vector2.up * jumpForce);
			_jumpTimes++;
		}

		float forceThisFrame = horizontalMultiplier * Time.deltaTime;
		float velocityY = _rigidBody.velocity.y;
		
		if (Input.GetKey(KeyCode.A))
		{
			if (velocityLastFrame == -1)
			{
				_rigidBody.AddForce(Vector2.left * forceThisFrame);
			}
			else
			{
				_rigidBody.velocity = new Vector2(0,velocityY);
				_rigidBody.AddForce(Vector2.left * forceThisFrame);
			}
			
			velocityLastFrame = -1;
		}

		if (Input.GetKey(KeyCode.D))
		{
			if (velocityLastFrame == 1)
			{
				_rigidBody.AddForce(Vector2.right * forceThisFrame);
			}
			else
			{
				_rigidBody.velocity = new Vector2(0,velocityY);
				_rigidBody.AddForce(Vector2.right * forceThisFrame);
			}
			
			velocityLastFrame = 1;
		}
		
		float velocityX = _rigidBody.velocity.x;
		float clampedVelocityX;
		clampedVelocityX = Mathf.Clamp(velocityX, -maxSpeedX, maxSpeedX);
		_rigidBody.velocity = new Vector2(clampedVelocityX,velocityY);
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("unfriendly"))
		{
			Die();
		}
		if (collision.gameObject.CompareTag("branch"))
		{
			_jumpTimes = 0;
		}
		if (collision.gameObject.CompareTag("Player"))
		{
			_jumpTimes = 0;
			if ((!isAlive) && hasLastWords)
			{
				Instantiate(Resources.Load<GameObject>("Prefabs/lastWords"), transform.position + new Vector3(0,2,0), Quaternion.identity);
				hasLastWords = false;
			}
		}

		
	}

	public void Die()
	{
		isAlive = false;
		_spriteRenderer.color = new Color(1f,1f,1f,0.5f);
		
	}

	void RespondToPlantSeed()
	{
		if (Input.GetKeyDown(KeyCode.S) && hasSeed)
		{
			Instantiate(Resources.Load<GameObject>("Prefabs/seed"), transform.position, Quaternion.identity);
			hasSeed = false;
		}
	}
	
	private void GiveBirth()
	{
		var parentPlayer = Services.Players[Services.Players.Count - 1];
		parentPlayer.hasSeed = false;
		parentPlayer.Invoke("Die", Services.Control.separateTime);
		
		var newPlayerObj = Instantiate(Resources.Load<GameObject>("Prefabs/Player"), parentPlayer.transform.position, parentPlayer.transform.rotation);
		newPlayerObj.name = "player " + Services.Players.Count;
		newPlayerObj.transform.localScale = Services.Control.playerStartSize * Vector3.one;
		var newPlayer = newPlayerObj.AddComponent<Player>();
		Services.Players.Add(newPlayer);

	}
	

}
