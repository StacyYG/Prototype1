using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintLastWords : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		int n = Random.Range(0, 5);
		switch (n)
		{
			case 0:
				GetComponent<TextMesh>().text = "Good Job";
				break;
			case 1:
				GetComponent<TextMesh>().text = "Keep going";
				break;
			case 2:
				GetComponent<TextMesh>().text = "I'm proud of you";
				break;
			case 3:
				GetComponent<TextMesh>().text = "YAYYY";
				break;
			case 4:
				GetComponent<TextMesh>().text = "Up Up!";
				break;
				
		
			
		}

	}

}
