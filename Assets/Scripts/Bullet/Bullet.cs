using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Destroyble _destroyble;

	private void Awake()
	{
		_destroyble = GetComponent<Destroyble>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag(TagManager.GetTagNameByEnum(TagEnum.Platform)))
		{
			var platform = other.gameObject.GetComponent<Platform>();
			OnPlatformCollision(platform);
		}
	}

	private void OnPlatformCollision(Platform platform)
	{
		platform.Disable();
		_destroyble.Destroy();
	}
}
