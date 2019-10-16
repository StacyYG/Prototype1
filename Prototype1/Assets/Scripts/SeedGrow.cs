using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGrow : MonoBehaviour
{

	[SerializeField] private GameObject tree;
	[SerializeField] private float sproutTime = 5f;

	// Use this for initialization
	void Start () {
		Invoke("GrowToATree",sproutTime);
		
	}
	

	// Update is called once per frame
	void Update () {
		
	}

	void GrowToATree()
	{
		Instantiate(tree, transform.position, Quaternion.identity);
	}
}
