using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PrintWords : MonoBehaviour
{
	[FormerlySerializedAs("lastWords")] [SerializeField] private List<string> words = new List<string>();

	void Start ()
	{
		GetComponent<TextMeshPro>().text = words[Random.Range(0, words.Count)];
	}

	private void LateUpdate()
	{
		transform.rotation = Quaternion.Euler(0f, 0f, -transform.parent.rotation.z);
	}
}
