using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrow : MonoBehaviour
{
	[SerializeField] GameObject branch;
	[SerializeField] int growNewBranchInterval = 10;
	[SerializeField] int growBranchTotalTime = 30;
	[SerializeField] GameObject treeTop;
	[SerializeField] private float treeGrowRange = 4;
	[SerializeField] private int branchNumber = 5;
	public Transform right;
	public Transform left;
	
	Vector2 branchPosition;
	float randomNumber;

	
	

	// Use this for initialization
	void Start ()
	{
		Instantiate(branch, treeTop.transform.position, right.rotation);
		InvokeRepeating("GrowNewBranch",0,growNewBranchInterval);
		Invoke("StopGrowBranch",growBranchTotalTime);

	}

	void Awake()
	{
		right= GameObject.Find("RIGHT").transform;
		left = GameObject.Find("LEFT").transform;
	}

	// Update is called once per frame
	void Update ()
	{

		
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

	void StopGrowBranch()
	{
		CancelInvoke("GrowNewBranch");
	}
	
}
