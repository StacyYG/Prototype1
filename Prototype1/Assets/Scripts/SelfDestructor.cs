using UnityEngine;

public class SelfDestructor : MonoBehaviour {
	
	[SerializeField] private float startTime = 3f;
	
	void Start ()
	{
		Destroy(gameObject, startTime);
	}
}
