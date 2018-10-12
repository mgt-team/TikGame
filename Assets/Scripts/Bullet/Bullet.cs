using UnityEngine;

public class Bullet : Photon.MonoBehaviour
{
	private Destroyble _destroyble;

    private Vector3 _selfPosition;

    [SerializeField]
    private PhotonView _photonView;

	private void Awake()
	{
		_destroyble = GetComponent<Destroyble>();
        _photonView = GetComponent<PhotonView>();
	}

    private void Update()
    {
        if (!_photonView.isMine)
            SmoothNetMovement();
    }

    private void SmoothNetMovement()
    {
        transform.position = Vector3.Lerp(transform.position, _selfPosition, Time.deltaTime);
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
		platform.DisableForPhoton();
		_destroyble.Destroy();
	}

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
            stream.SendNext(transform.position);
        else
            _selfPosition = (Vector3)stream.ReceiveNext();
    }
}
