using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchGrowth : MonoBehaviour
{
	[SerializeField] private float branchGrowMultiplierX = 0.1f;
	[SerializeField] private float branchGrowMultiplierY = 0.01f;
	[SerializeField] private bool cancelRigidbody = false;
	

	private TreeGrowControl _treeGrowControl;


	// Use this for initialization
	void Start ()
	{
		_treeGrowControl = GetComponentInParent<TreeGrowControl>();
		if (!cancelRigidbody)
		{
			AddBoxCollider();
			AddRigidBody();
		}


	}



	// Update is called once per frame
	void Update () {
		BranchGrow();
		
	}
	private void BranchGrow()
	{
		if (_treeGrowControl.isGrowing)
		{
			gameObject.transform.localScale = gameObject.transform.localScale + 
			                                  new Vector3(branchGrowMultiplierX * Time.deltaTime,
				                                  branchGrowMultiplierY * Time.deltaTime, 0);
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
