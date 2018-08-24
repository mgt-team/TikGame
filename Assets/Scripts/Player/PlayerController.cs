using UnityEngine;

public class PlayerController : MonoBehaviour
{

	[SerializeField]
	private DirectionOnPlatformController _directionOnPlatformController;
	
	// Update is called once per frame
	private void Update ()
	{
		var targetPlatform = _directionOnPlatformController.GetTargetPlatform();
		if (Input.GetMouseButtonDown(1) && targetPlatform != null 
		                                && !targetPlatform.IsEnabled())
		{
			targetPlatform.Enable();
		}
	}
}
