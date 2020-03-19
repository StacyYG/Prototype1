using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGrow : MonoBehaviour
{
	private float sproutTime = 5f;

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
		var newTree = new GameObject();
		newTree.name = "tree";
		newTree.transform.position = transform.position;
		newTree.AddComponent<TreeGrowControl>();
		gameObject.transform.parent = newTree.transform;
	}

	private IEnumerator WaitAndGrowToTree(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		GrowToTree();
	}
}
