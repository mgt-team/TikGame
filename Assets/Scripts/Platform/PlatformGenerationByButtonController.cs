using UnityEngine;

public class PlatformGenerationByButtonController : MonoBehaviour {

	[SerializeField]
	private PlatformManager _platformManager;
	
	// Update is called once per frame
	private void Update () {
		if (Input.GetKeyDown(KeyCode.G))
		{
			_platformManager.EnableRandomPlatform();
		}
	}
}
