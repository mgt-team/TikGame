using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private DirectionOnPlatformController _directionOnPlatformController;

    [SerializeField]
    private Gun _gun;

    [SerializeField]
    private MouseButton _generateMouseButton;

    [SerializeField]
    private MouseButton _shootMouseButton;

    public delegate void MethoodContainer();
    public event MethoodContainer ActionCommitted;

    [SerializeField]
    private float _shootCooldown;

    [ReadOnly]
    [SerializeField]
    private float _shootTimer = 0;

    private void Start()
    {
        ActionCommitted += PlayerController_ActionCommitted;
    }

    private void PlayerController_ActionCommitted()
    {
        _shootTimer = _shootCooldown;
    }

    private void Update()
    {
        if (_shootTimer <= 0)
        {
            _directionOnPlatformController.WhiteMaterial();
            ApproveOfAction();
        }
        else if (_shootTimer > 0)
        {
            _directionOnPlatformController.RedMaterial();
            _shootTimer -= Time.deltaTime;
        }
    }

    public void ApproveOfAction()
    {
        var targetPlatform = _directionOnPlatformController.GetTargetPlatform();
        if (Input.GetMouseButton(_generateMouseButton.GetHashCode()) && targetPlatform != null
                                        && !targetPlatform.IsEnabled())
        {
            targetPlatform.Enable();
            ActionCommitted();
        }

        if (Input.GetMouseButton(_shootMouseButton.GetHashCode()))
        {
            _gun.Shoot();
            ActionCommitted();
        }
    }
}
