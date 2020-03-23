using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour {

	private Transform _player;
	public Vector2 margin;
	public Vector2 smoothing;

	public bool isFollowing;

	private float _halfHeight;
	private float _halfWidth;
	
	private void Start()
	{
		isFollowing = true;
		_player = Services.Players.Last().transform;
		Services.EventManager.Register<NewPlayerBorn>(OnNewPlayerBorn);
		_halfHeight = GetComponent<Camera>().orthographicSize;
		_halfWidth = _halfHeight * Screen.width / Screen.height;
	}
	
	private void OnDestroy()
	{
		Services.EventManager.Unregister<NewPlayerBorn>(OnNewPlayerBorn);
	}

	private void Update()
	{
		var currentPos = transform.position;
		var x = currentPos.x;
		var y = currentPos.y;
		if (isFollowing) {
			if (Mathf.Abs (x - _player.position.x) > margin.x) {
				x = Mathf.Lerp (x,_player.position.x,smoothing.x*Time.deltaTime);
			}
			if (Mathf.Abs (y - _player.position.y)> margin.y) {
				y = Mathf.Lerp (y,_player.position.y,smoothing.y*Time.deltaTime);
			}
		}

		transform.position = new Vector3(x, y, currentPos.z);
	}

	private void OnNewPlayerBorn(AGPEvent e)
	{
		_player = Services.Players.Last().transform;
	}

	private void ZoomOut()
	{
		
	}
}
