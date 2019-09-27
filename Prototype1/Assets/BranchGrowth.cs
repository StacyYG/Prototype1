using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchGrowth : MonoBehaviour
{
	[SerializeField] private Transform parent;
	[SerializeField] private float branchEndLength = 30f;
	[SerializeField] private float branchGrowMultiplier = 0.01f;
	[SerializeField] private bool cancelRigidbody = false;
	[SerializeField] private bool cancelParent = false;


	// Use this for initialization
	void Start ()
	{
		if (!cancelParent)
		{
			gameObject.transform.parent = parent;
		}
		
		if (!cancelRigidbody)
		{
			AddBoxCollider();
			AddRigidBody();
		}


	}

	void Awake()
	{
		parent = GameObject.Find("tree").transform;
	}

	// Update is called once per frame
	void Update () {
		BranchGrow();
		
	}
	private void BranchGrow()
	{
		if (gameObject.transform.localScale.x < branchEndLength)
		{
			gameObject.transform.localScale = gameObject.transform.localScale + new Vector3(branchGrowMultiplier * Time.deltaTime, 0, 0);
		}
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
