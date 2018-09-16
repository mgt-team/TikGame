using UnityEngine;

public class DirectionOnPlatformController : MonoBehaviour {

    [SerializeField]
    private Platform _targetPlatform;

    [SerializeField] 
    private Material _directedMaterial;

    private RaycastHit _hit;
    private Transform _transform;

    private Color _red = new Vector4(1, 0, 0, 0.2f);
    private Color _yellow = new Vector4(1, 1, 0, 0.2f);

    private void Start()
    {
        _transform = gameObject.transform;
    }

    private void FixedUpdate()
    {
        Vector3 direction = _transform.TransformDirection(Vector3.forward);
        // Check ray intersection with object 
        if (Physics.Raycast(_transform.position, direction, out _hit, 1000f))
        {
            // Check if intersected object is Platform
            if (_hit.transform.CompareTag(TagManager.GetTagNameByEnum(TagEnum.Platform)))
            {
                // Check if it not already directed platform
                if (_targetPlatform != _hit.transform.GetComponent<Platform>())
                {
                    // Clean previous platform
                    if (_targetPlatform != null)
                    {
                        _targetPlatform.OnOutFromDirection();
                    }

                    // Direct on new platform
                    _targetPlatform = _hit.transform.GetComponent<Platform>();
                    _targetPlatform.OnDirected(_directedMaterial);
                }
            }
        }
        else
        {
            // Check if not directed on saved platform
            if (_targetPlatform != null)
            {
                _targetPlatform.OnOutFromDirection();
                _targetPlatform = null;
            }
        }
    }

    public void RedMaterial()
    {
        _directedMaterial.color = _red;
    }

    public void YellowMaterial()
    {
        _directedMaterial.color = _yellow;
    }

    public Platform GetTargetPlatform()
    {
        return _targetPlatform;
    }
}
