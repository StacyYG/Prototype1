using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
	private Rigidbody2D _rb;
	private SpriteRenderer _spriteRenderer;
	private int _jumpTimes;
	private int _maxJumpTimes = 2;
	private float _jumpForce = 450f;
	private float _horizontalMultiplier = 1500f;
	private float _maxSpeedX = 7f;
	private float _playerGrowMultiplier = 0.1f;
	public bool isAlive = true;
	public bool hasSeed = true;
	public bool hasLastWords = true;
	private int _velocityLastFrame;
	private Vector2 _lastVelocity;
	private Vector2 _smoothing = new Vector2(1f, 1f);

	private void Awake()
	{
		Services.Players.Add(this);
	}

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
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
		var currentV = _rb.velocity;
		if (Input.GetKeyDown(KeyCode.Space) && _jumpTimes < _maxJumpTimes)
		{
			_rb.velocity = new Vector2(currentV.x, 0f);
			_rb.AddForce(Vector2.up * _jumpForce);
			_jumpTimes++;
		}

		var forcePerFrame = _horizontalMultiplier * Time.deltaTime;

		if (Input.GetKey(KeyCode.A))
		{
			if (currentV.x <= 0)
			{
				if (Mathf.Abs(currentV.x) < _maxSpeedX)
				{
					_rb.AddForce(Vector2.left * forcePerFrame);
				}
			}
			else
			{
				_rb.velocity = new Vector2(0, currentV.y);
			}
		}

		if (Input.GetKey(KeyCode.D))
		{
			if (currentV.x >= 0)
			{
				if (Mathf.Abs(currentV.x) < _maxSpeedX)
				{
					_rb.AddForce(Vector2.right * forcePerFrame);
				}
			}
			else
			{
				_rb.velocity = new Vector2(0, currentV.y);
			}
		}
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
				Instantiate(Resources.Load<GameObject>("Prefabs/lastWords"),
					transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity, transform);
				hasLastWords = false;
			}
		}
	}

	private void Die()
	{
		isAlive = false;
		hasSeed = false;
		_spriteRenderer.color = new Color(1f,1f,1f,0.5f);

	}

	private IEnumerator WaitAndDie(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Die();
	}

	private void RespondToPlantSeed()
	{
		if (Input.GetKeyDown(KeyCode.S) && hasSeed)
		{
			Instantiate(Resources.Load<GameObject>("Prefabs/seed"), transform.position, Quaternion.identity);
			hasSeed = false;
		}
	}
	
	private void GiveBirth()
	{
		if (!isAlive) return;
		hasSeed = false;
		StartCoroutine(WaitAndDie(Services.Control.separateTime));
		
		var newPlayerObj = Instantiate(Resources.Load<GameObject>("Prefabs/Player"), transform.position, transform.rotation);
		newPlayerObj.name = "player " + Services.Players.Count;
		newPlayerObj.transform.localScale = Services.Control.playerStartSize * Vector3.one;
		Services.EventManager.Fire(new NewPlayerBorn());
	}

	private IEnumerator WaitAndGiveBirth(float interval)
	{
		yield return new WaitForSeconds(interval);
		GiveBirth();
	}
	
}

public class NewPlayerBorn : AGPEvent{}
