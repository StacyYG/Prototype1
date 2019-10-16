using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkGrowth : MonoBehaviour
{
	[SerializeField] private float treeGrowMultlplier = 0.3f;
	private TreeGrowControl _treeGrowControl;
	

	// Use this for initialization
	void Start ()
	{
		_treeGrowControl = GetComponentInParent<TreeGrowControl>();


	}

	// Update is called once per frame
	void Update ()
	{
		TreeGrow();


	}

	private void TreeGrow()
	{
		if (_treeGrowControl.isGrowing)
		{
			gameObject.transform.localScale = gameObject.transform.localScale + 
			                                  new Vector3(0, treeGrowMultlplier * Time.deltaTime, 0);
		}
	}
	
	
}
