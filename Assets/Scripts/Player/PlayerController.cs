using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerController : MonoBehaviour
{

	[SerializeField]
	private DirectionOnPlatformController _directionOnPlatformController;

	[SerializeField] 
	private MouseButton _mouseButton;
	
	// Update is called once per frame
	private void Update ()
	{
		var targetPlatform = _directionOnPlatformController.GetTargetPlatform();
		if (Input.GetMouseButtonDown(_mouseButton.GetHashCode()) && targetPlatform != null 
		                                && !targetPlatform.IsEnabled())
		{
			targetPlatform.Enable();
		}
	}
}
