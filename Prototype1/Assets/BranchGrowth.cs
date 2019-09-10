using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchGrowth : MonoBehaviour
{
	[SerializeField] Transform parent;
	[SerializeField] float branchEndLength = 30f;
	[SerializeField] float branchGrowMultiplier = 0.01f;

	private int growth = 0;


	// Use this for initialization
	void Start ()
	{
		gameObject.transform.parent = parent;

	}
	
	// Update is called once per frame
	void Update () {
		BranchGrow();
		
	}
	private void BranchGrow()
	{
		float currentLength = gameObject.transform.localScale.x;
		if (currentLength < branchEndLength)
		{
			gameObject.transform.localScale = new Vector3(currentLength + branchGrowMultiplier * Time.deltaTime, 1, 1);
			growth++;
		}
	}
}
