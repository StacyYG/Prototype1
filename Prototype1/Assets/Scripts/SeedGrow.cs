using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGrow : MonoBehaviour
{
	private float sproutTime = 5f;
	public GameObject newTree;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(WaitAndGrowToTree(sproutTime));
	}
	

	// Update is called once per frame
	void Update () {
		
	}

	private void GrowToTree()
	{
		var pos = transform.position;
		var tree = Instantiate(newTree, pos, Quaternion.identity);
		transform.parent = tree.transform;
	}

	private IEnumerator WaitAndGrowToTree(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		GrowToTree();
	}
}
