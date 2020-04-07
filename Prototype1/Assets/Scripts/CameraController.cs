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
	private Camera _myCam;
	private const float ZoomOutTime = 10f;
	private float _interval = 0.005f;
	public int _maxIndex;

	private void Start()
	{
		_maxIndex = (int)(ZoomOutTime / _interval);
		isFollowing = true;
		_player = Services.Players.Last().transform;
		Services.EventManager.Register<NewPlayerBorn>(OnNewPlayerBorn);
		_myCam = GetComponent<Camera>();
		_halfHeight = _myCam.orthographicSize;
		_halfWidth = _halfHeight * Screen.width / Screen.height;
		Services.CompareWithTreesBound(new Vector2(0f, -_halfHeight));
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
			if (Mathf.Abs (x - _player.position.x) > margin.x)
			{
				x = Mathf.Lerp(x, _player.position.x, smoothing.x * Time.deltaTime);
			}
			if (Mathf.Abs (y - _player.position.y)> margin.y)
			{
				y = Mathf.Lerp(y, _player.position.y, smoothing.y * Time.deltaTime);
			}
		}

		transform.position = new Vector3(x, y, currentPos.z);
		
		if (Input.GetKey(KeyCode.I) || Services.Control.WaitForReload)
		{
			isFollowing = false;
			ZoomOut();
		}

		if (Input.GetKeyUp(KeyCode.I))
		{
			_myCam.orthographicSize = _halfHeight;
			var playerPos = _player.position;
			_myCam.transform.position = new Vector3(playerPos.x, playerPos.y, currentPos.z);
			isFollowing = true;
		}
		
	}

	private void OnNewPlayerBorn(AGPEvent e)
	{
		_player = Services.Players.Last().transform;
	}

	private void LerpCameraSizeAndPosition(float stepCamSize, float stepPosX, float stepPosY)
	{
		var xRatio = Services.TreesBound.Width / (_halfWidth * 2f);
		var yRatio = Services.TreesBound.Height / (_halfHeight * 2f);
		var maxRatio = Mathf.Max(xRatio, yRatio);
		var endPos = Services.TreesBound.MidPoint;
		
		_myCam.orthographicSize = Mathf.Lerp(_myCam.orthographicSize, _halfHeight * maxRatio, stepCamSize);
		var currentPos = transform.position;
		var x = Mathf.Lerp(currentPos.x, endPos.x, stepPosX);
		var y = Mathf.Lerp(currentPos.y, endPos.y, stepPosY);
		transform.position = new Vector3(x, y, currentPos.z);
	}
	private void ZoomOut()
	{
		LerpCameraSizeAndPosition(smoothing.y * Time.deltaTime, smoothing.x * Time.deltaTime, smoothing.y * Time.deltaTime);
	}
}
