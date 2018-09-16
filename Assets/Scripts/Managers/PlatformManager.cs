using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
	[SerializeField] 
	private PlatformMap _platformMap;
	[SerializeField] 
	private Platform _finishPlatform;
	[SerializeField] 
	private List<Platform> _startPlatforms;

	public void InitPlatformMap()
	{
		// Disable all platforms on map
		foreach (var platform in _platformMap.GetPlatforms())
		{
			platform.Disable();
		}
		
		
		// Enable start and finish platforms
		_finishPlatform.Enable();
		foreach (var platform in _startPlatforms)
		{
			platform.Enable();
		}
	}

	public void EnableRandomPlatform()
	{
		var enabledPlatform = GetRandomEnabledPlatformWithDisabledNeighbor();
		
		// Do nothing if there is no any disabled platforms in game
		if (enabledPlatform == null)
		{
			return;
		}

		enabledPlatform.GetRandomDisabledPlatform().Enable();
	}

	private Platform GetRandomEnabledPlatformWithDisabledNeighbor()
	{
		var enabledPlatforms = GetEnabledPlatformsWithDisabledNeighbor();

		// Check if there is no any disabled platforms in game
		if (enabledPlatforms.Count == 0)
		{
			Debug.Log("There is no any disabled platforms in game to enable it");
			return null;
		}
		else
		{
			return enabledPlatforms[Random.Range(0, enabledPlatforms.Count)];	
		}
	}

	private List<Platform> GetEnabledPlatformsWithDisabledNeighbor()
	{
		return _platformMap.GetPlatforms()
			.FindAll(platform => platform.IsEnabled() && platform.HasDisabledNeighbor());
	}

    public List<Transform> GetStartedPlatforms()
    {
        List<Transform> transforms = new List<Transform>();
        foreach(Platform platform in _startPlatforms)
        {
            transforms.Add(platform.GetTransform());
        }
        return transforms;
    }
}
