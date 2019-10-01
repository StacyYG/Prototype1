﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGrow : MonoBehaviour
{

	[SerializeField] private GameObject tree;
	public Transform treeRotate;

	// Use this for initialization
	void Start () {
		Invoke("GrowToATree",5f);
		
	}
	

	// Update is called once per frame
	void Update () {
		
	}

	void GrowToATree()
	{
		Instantiate(tree, gameObject.transform.position, treeRotate.rotation);
	}
}