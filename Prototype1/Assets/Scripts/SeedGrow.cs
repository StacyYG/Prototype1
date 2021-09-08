using System.Collections;
using UnityEngine;

public class SeedGrow : MonoBehaviour
{
	private float sproutTime = 5f;
	[SerializeField] private GameObject newTree;
	
	void Start ()
	{
		StartCoroutine(GrowToTree(sproutTime));
	}

	private void GrowToTree()
	{
		var pos = transform.position;
		var tree = Instantiate(newTree, pos, Quaternion.identity);
		Destroy(gameObject);
	}

	private IEnumerator GrowToTree(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		GrowToTree();
	}
}
