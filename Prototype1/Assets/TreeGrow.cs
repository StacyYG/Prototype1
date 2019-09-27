using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrow : MonoBehaviour
{
	[SerializeField] private GameObject branch;
	[SerializeField] private int growNewBranchInterval = 10;
	[SerializeField] private int growBranchTotalTime = 30;
	[SerializeField] private GameObject treeTop;
	[SerializeField] private float treeGrowRange = 4;

	public Transform right;
	public Transform left;
	
	private Vector2 branchPosition;
	private float randomNumber;

	
	

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
