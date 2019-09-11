using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
	[SerializeField] GameObject player;
	[SerializeField] GameObject trunk;
	[SerializeField] GameObject branch;
	[SerializeField] int growNewBranchInterval = 10;
	[SerializeField] GameObject treeTop;
	[SerializeField] private float treeGrowRange = 4;
	public Transform right;
	public Transform left;
	
	Vector2 branchPosition;
	float randomNumber;
	int branchIndexLastFrame = 0;
	
	

	// Use this for initialization
	void Start ()
	{


	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckTimeForNewBranch();
		
	}

	private void CheckTimeForNewBranch()
	{
		int branchIndex = (int)Time.time / growNewBranchInterval;

		if (branchIndex > branchIndexLastFrame)
		{
			GrowNewBranch();
		}

		branchIndexLastFrame = branchIndex;
	}

	private void GrowNewBranch()
	{
		randomNumber = Random.Range(-treeGrowRange, treeGrowRange);
		branchPosition = treeTop.transform.position + new Vector3(randomNumber,0);
		
		if (randomNumber > 0)
		{
			Instantiate(branch, branchPosition, right.rotation);
		}
		else
		{
			Instantiate(branch, branchPosition, left.rotation);
		}
	}
	
}
