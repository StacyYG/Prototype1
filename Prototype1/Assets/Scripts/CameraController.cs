using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class CameraController : MonoBehaviour {

	private Transform player;
	public Vector2 Margin;
	public Vector2 smoothing;
	
	private Vector3 _min;
	private Vector3 _max;

	public bool IsFollowing;
	
	private void Start()
	{
		IsFollowing = true;
		player = Services.Players.Last().transform;
		Services.EventManager.Register<NewPlayerBorn>(OnNewPlayerBorn);

	}
	
	private void OnDestroy()
	{
		Services.EventManager.Unregister<NewPlayerBorn>(OnNewPlayerBorn);
	}

	private void Update()
	{
		var x = transform.position.x;
		var y = transform.position.y;
		if (IsFollowing) {
			if (Mathf.Abs (x - player.position.x) > Margin.x) {
				x = Mathf.Lerp (x,player.position.x,smoothing.x*Time.deltaTime);
			}
			if (Mathf.Abs (y - player.position.y)> Margin.y) {
				y = Mathf.Lerp (y,player.position.y,smoothing.y*Time.deltaTime);
			}
		}

		transform.position = new Vector3 (x,y,transform.position.z);
	}

	private void OnNewPlayerBorn(AGPEvent e)
	{
		player = Services.Players.Last().transform;
	}
}
