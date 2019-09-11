using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour {
	
	[SerializeField] float startTime = 3f;

	// Use this for initialization
	void Start () {
		Destroy(gameObject,startTime);
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
