using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
	private Rigidbody2D _rigidBody;
	private SpriteRenderer _spriteRenderer;
	private int _jumpTimes;
	private int _maxJumpTimes = 2;
	private float _jumpForce = 700f;
	private float _horizontalMultiplier = 4000f;
	private float _maxSpeedX = 12f;
	private float _playerGrowMultiplier = 0.1f;
	public bool isAlive = true;
	private bool _hasSeed = true;
	private bool _hasLastWords = true;
	private int _velocityLastFrame;

	private void Start()
	{
		_rigidBody = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine(WaitAndGiveBirth(Services.Control.giveBirthInterval));
	}

	// Update is called once per frame
	private void Update() 
	{
		if (isAlive)
		{
			RespondToMovement();
			RespondToPlantSeed();
			if (gameObject.transform.localScale.x < 1)
			{
				gameObject.transform.localScale += _playerGrowMultiplier * Time.deltaTime * Vector3.one;
			}
		}
		
	}

	private void RespondToMovement()
	{
		
		if (Input.GetKeyDown(KeyCode.Space) && _jumpTimes < _maxJumpTimes)
		{
			_rigidBody.AddForce(Vector2.up * _jumpForce);
			_jumpTimes++;
		}

		var forcePerFrame = _horizontalMultiplier * Time.deltaTime;
		var velocityY = _rigidBody.velocity.y;
		
		if (Input.GetKey(KeyCode.A))
		{
			if (_velocityLastFrame == -1)
			{
				_rigidBody.AddForce(Vector2.left * forcePerFrame);
			}
			else
			{
				_rigidBody.velocity = new Vector2(0, velocityY);
				_rigidBody.AddForce(Vector2.left * forcePerFrame);
			}
			
			_velocityLastFrame = -1;
		}

		if (Input.GetKey(KeyCode.D))
		{
			if (_velocityLastFrame == 1)
			{
				_rigidBody.AddForce(Vector2.right * forcePerFrame);
			}
			else
			{
				_rigidBody.velocity = new Vector2(0,velocityY);
				_rigidBody.AddForce(Vector2.right * forcePerFrame);
			}
			
			_velocityLastFrame = 1;
		}
		
		float velocityX = _rigidBody.velocity.x;
		float clampedVelocityX;
		clampedVelocityX = Mathf.Clamp(velocityX, -_maxSpeedX, _maxSpeedX);
		_rigidBody.velocity = new Vector2(clampedVelocityX, velocityY);

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
			if ((!isAlive) && _hasLastWords)
			{
				Instantiate(Resources.Load<GameObject>("Prefabs/lastWords"), transform.position + new Vector3(0,2,0), Quaternion.identity);
				_hasLastWords = false;
			}
		}

		
	}

	private void Die()
	{
		isAlive = false;
		_spriteRenderer.color = new Color(1f,1f,1f,0.5f);

	}

	private IEnumerator WaitAndDie(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Die();
	}

	private void RespondToPlantSeed()
	{
		if (Input.GetKeyDown(KeyCode.S) && _hasSeed)
		{
			Instantiate(Resources.Load<GameObject>("Prefabs/seed"), transform.position, Quaternion.identity);
			_hasSeed = false;
		}
	}
	
	private void GiveBirth()
	{
		if (!isAlive) return;
		_hasSeed = false;
		StartCoroutine(WaitAndDie(Services.Control.separateTime));
		
		var newPlayerObj = Instantiate(Resources.Load<GameObject>("Prefabs/Player"), transform.position, transform.rotation);
		newPlayerObj.name = "player " + Services.Players.Count;
		newPlayerObj.transform.localScale = Services.Control.playerStartSize * Vector3.one;
		var newPlayer = newPlayerObj.AddComponent<Player>();
		Services.Players.Add(newPlayer);
	}

	private IEnumerator WaitAndGiveBirth(float interval)
	{
		yield return new WaitForSeconds(interval);
		GiveBirth();
	}

}
