using UnityEngine;
using System.Linq;

public class CameraController : MonoBehaviour 
{
	[SerializeField] private Vector2 posSmoothing;
	[SerializeField] private float sizeSmoothing;
	[SerializeField] private bool isFollowing = true;

	private float _halfHeight;
	private float _halfWidth;
	private Camera _myCam;
	private Transform _player;
	
	private void Start()
	{
		// Camera follows the last player. Update the target player everytime a new player is born
		_player = Services.Players.Last().transform;
		Services.EventManager.Register<PlayerBorn>(OnNewPlayerBorn);
		
		_myCam = GetComponent<Camera>();
		_halfHeight = _myCam.orthographicSize;
		_halfWidth = _halfHeight * Screen.width / Screen.height;
		
		// Make the tree range cover some space below the trees just to look better when zoomed out
		Services.TreeRange.Update(new Vector2(0f, -_halfHeight));
	}

	private void Update()
	{
		// Lerp
		if (isFollowing)
		{
			LerpCameraPosition(_player.position, Time.deltaTime * posSmoothing);
			LerpCameraSize(_halfHeight, sizeSmoothing * Time.deltaTime);
		}

		// Zoom out when holding Q key or when game over
		if (Input.GetKey(KeyCode.Q) || Services.Control.GameOver)
		{
			isFollowing = false;
			ZoomOut();
			
			// All players say some happy words when the camera zooms out
			foreach (var player in Services.Players)
			{
				player.SayHappyWords();
			}
		}

		// Resume from zoom-out mode when letting go the Q key
		if (Input.GetKeyUp(KeyCode.Q))
		{
			isFollowing = true;
		}
	}

	private void LerpCameraPosition(Vector2 targetPosition, Vector2 stepPos)
	{
		var x = Mathf.Lerp(transform.position.x, targetPosition.x, stepPos.x);
		var y = Mathf.Lerp(transform.position.y, targetPosition.y, stepPos.y);
		transform.position = new Vector3(x, y, transform.position.z);
	}

	private void LerpCameraSize(float targetSize, float stepCamSize)
	{
		_myCam.orthographicSize = Mathf.Lerp(_myCam.orthographicSize, targetSize, stepCamSize);
	}

	private float ZoomOutRatio()
	{
		var xRatio = Services.TreeRange.Width / (_halfWidth * 2f);
		var yRatio = Services.TreeRange.Height / (_halfHeight * 2f);
		return Mathf.Max(xRatio, yRatio); // Make sure all the trees are covered in the zoom-out view
	}

	private void ZoomOut()
	{
		LerpCameraSize(_halfHeight * ZoomOutRatio(), sizeSmoothing * Time.deltaTime); // Zoom out the camera to cover all the trees
		LerpCameraPosition(Services.TreeRange.MidPoint,
			Time.deltaTime * posSmoothing); // Move the camera to the center of all the trees
	}
	
	private void OnNewPlayerBorn(AGPEvent e)
	{
		_player = Services.CurrentPlayer.transform;
	}
	
	private void OnDestroy()
	{
		Services.EventManager.Unregister<PlayerBorn>(OnNewPlayerBorn);
	}
}
