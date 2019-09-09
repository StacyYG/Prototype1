using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using Debug = System.Diagnostics.Debug;

public class Player : MonoBehaviour
{
	Rigidbody2D rigidBody;
	int currentSceneIndex = 0;
	[SerializeField] int maxJumpTimes = 2;
	[SerializeField] float jumpForce = 400f;
	[SerializeField] float horizontalMultiplier = 200f;
	[SerializeField] float maxSpeedX = 12f;

	private float velocityLastFrame;
	enum State
	{
		Alive, Dying
	}
	State state = State.Alive;
	
	int jumpTimes = 0;
	
	// Use this for initialization
	void Start ()
	{
		rigidBody = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.Alive)
		{
			RespondToMovement();
		}

		if (UnityEngine.Debug.isDebugBuild)
		{
			RespondToDebugKeys();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			ReloadLevel();
		}
	}

	void RespondToDebugKeys()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			LoadNextLevel();
		}
	}

	void ReloadLevel()
	{
		SceneManager.LoadScene(currentSceneIndex);
	}
	
	private void LoadNextLevel()
	{
		int nextSceneIndex = currentSceneIndex + 1;
		if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
		{
			nextSceneIndex = 0;
		}
		SceneManager.LoadScene(nextSceneIndex);
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

	private void OnCollisionEnter2D(Collision2D other)
	{
		jumpTimes = 0;
	}
}
