using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
	[SerializeField] GameObject branch;
	[SerializeField] Transform parent;
	[SerializeField] bool cancelRigidbody = false;


	// Use this for initialization
	void Start ()
	{
		branch.transform.parent = parent;
		if (!cancelRigidbody)
		{
			AddBoxCollider();
			AddRigidBody();
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	

	private void AddBoxCollider()
	{
		if (gameObject.GetComponent<BoxCollider2D>() == null)
		{
			Collider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
			boxCollider.isTrigger = false;
		}

	}

	private void AddRigidBody()
	{
		if (gameObject.GetComponent<Rigidbody2D>() == null)
		{
			Rigidbody2D rigidBody = gameObject.AddComponent<Rigidbody2D>();
			rigidBody.bodyType = RigidbodyType2D.Static;
			rigidBody.simulated = true;
		}
	}
	
}
