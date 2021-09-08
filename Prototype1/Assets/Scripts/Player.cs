using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	#region Parameters
	
	[SerializeField] private float _jumpForce = 450f;
	[SerializeField] private float _horizontalMultiplier = 1500f;
	[SerializeField] private float _maxSpeedX = 7f;
	[SerializeField] private float _playerGrowMultiplier = 0.1f;
	
	// BirthInterval: Player give birth interval
	[SerializeField] private int birthInterval = 9;
	
	// BornSize: Baby's size when born
	[SerializeField] private float bornSize = 0.3f;
	
	// SeparateTime: How long it takes the baby to separate from the parent
	[SerializeField] private float separateTime = 3f;
	
	#endregion

	#region Public status

	public bool isAlive = true;
	public bool hasSeed = true;

	#endregion

	#region Prefabs

	[SerializeField] private GameObject blessWords, happyWords;

	#endregion
	
	private bool _canJump = true;
	private Rigidbody2D _rb;
	private SpriteRenderer _spriteRenderer;

	private void Awake()
	{
		Services.Players.Add(this);
	}

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine(GiveBirth(birthInterval));
	}
	
	private void Update() 
	{
		if (isAlive)
		{
			Jump();
			Move();
			PlantSeed();
			GrowUp();
		}
	}

	private void Jump()
	{
		// Jump: when pressing SPACE, keep horizontal velocity, set vertical velocity to 0, and add vertical up force
		// JumpTimes: player can jump once in air
		if (Input.GetKeyDown(KeyCode.Space) && _canJump)
		{
			_rb.velocity = new Vector2(_rb.velocity.x, 0f);
			_rb.AddForce(Vector2.up * _jumpForce);
			_canJump = false;
		}
	}
	
	private void Move()
	{
		// Move left and right
		var forcePerFrame = _horizontalMultiplier * Time.deltaTime;

		if (Input.GetKey(KeyCode.A))
		{
			// if player is already moving left when pressing A, accelerate until it reaches the max speed
			if (_rb.velocity.x <= 0)
			{
				if (Mathf.Abs(_rb.velocity.x) < _maxSpeedX)
				{
					_rb.AddForce(Vector2.left * forcePerFrame);
				}
			}
			// if player is moving right when pressing A, set the horizontal velocity to 0
			else
			{
				_rb.velocity = new Vector2(0, _rb.velocity.y);
			}
		}

		if (Input.GetKey(KeyCode.D))
		{
			// if player is already moving right when pressing D, accelerate until it reaches the max speed
			if (_rb.velocity.x >= 0)
			{
				if (Mathf.Abs(_rb.velocity.x) < _maxSpeedX)
				{
					_rb.AddForce(Vector2.right * forcePerFrame);
				}
			}
			// if player is moving left when pressing D, set the horizontal velocity to 0
			else
			{
				_rb.velocity = new Vector2(0, _rb.velocity.y);
			}
		}
	}
	
	private void PlantSeed()
	{
		if (Input.GetKeyDown(KeyCode.S) && hasSeed)
		{
			Instantiate(Resources.Load<GameObject>("Prefabs/seed"), transform.position, Quaternion.identity);
			hasSeed = false;
		}
	}
	
	private void GrowUp()
	{
		if (gameObject.transform.localScale.x < 1)
		{
			gameObject.transform.localScale += _playerGrowMultiplier * Time.deltaTime * Vector3.one;
		}
	}

	private void Die()
	{
		isAlive = false;
		hasSeed = false;
		_spriteRenderer.color = new Color(1f,1f,1f,0.5f);
		tag = "deadPlayer";
	}

	private void GiveBirth()
	{
		if (!isAlive) return;
		hasSeed = false; // If player hasn't planted a tree before giving birth, the seed becomes invalid

		// Generate a new player
		var newPlayerObj = Instantiate(Resources.Load<GameObject>("Prefabs/Player"), transform.position, transform.rotation);
		newPlayerObj.name = "player " + Services.Players.Count;
		newPlayerObj.transform.localScale = bornSize * Vector3.one;
		Services.EventManager.Fire(new PlayerBorn());
		
		StartCoroutine(Die(separateTime));
	}

	private IEnumerator GiveBirth(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		GiveBirth();
	}
	
	private IEnumerator Die(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Die();
	}
	
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("unfriendly"))
		{
			Die();
		}
		
		// Touching a branch or a dead player enables the player to jump again
		else if (other.gameObject.CompareTag("branch") || other.gameObject.CompareTag("deadPlayer"))
		{
			_canJump = true;
		}
		
		// When a dead player is touched by the current player, it says some blessing words
		else if (CompareTag("deadPlayer") && other.gameObject.CompareTag("Player"))
		{
			SayWords(blessWords);
		}
	}

	private void SayWords(GameObject words)
	{
		if (gameObject.transform.localScale.x < 1) return; // If the player hasn't grown up, it cannot speak
		if (transform.childCount < 1) // Create a dialogue object attached to the player unless it is already saying something
		{
			Instantiate(words, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity, transform);
		}
	}

	public void SayHappyWords()
	{
		SayWords(happyWords);
	}
}

public class PlayerBorn : AGPEvent{}
