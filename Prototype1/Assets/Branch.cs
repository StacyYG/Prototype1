using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
	[SerializeField] GameObject branch;
	[SerializeField] Transform parent;


	// Use this for initialization
	void Start ()
	{
		branch.transform.parent = parent;
		AddBoxCollider();
		AddRigidBody();

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	

	private void AddBoxCollider()
	{
		Collider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
		boxCollider.isTrigger = false;
	}

	private void AddRigidBody()
	{
		Rigidbody2D rigidBody = gameObject.AddComponent<Rigidbody2D>();
		rigidBody.bodyType = RigidbodyType2D.Static;
		rigidBody.simulated = true;
	}
}
