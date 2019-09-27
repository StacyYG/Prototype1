using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFlower : MonoBehaviour
{
	//public GameObject player;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//GetComponent<ParticleSystem>().trigger.SetCollider();
		
	}

	private void OnParticleTrigger()
	{
		
		print("triggered");
		//ParticleSystem ps = GetComponent<ParticleSystem>();
		
		//List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
		
		
	}
}
