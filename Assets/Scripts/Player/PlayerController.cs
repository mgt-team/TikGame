using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private DirectionOnPlatformController _directionOnPlatformController;

    [SerializeField]
    private Gun _gun;

    [SerializeField]
    private MouseButton _mouseButtonForGenerate;

    [SerializeField]
    private MouseButton _mouseButtonForShoot;

    public delegate void MethoodContainer();
    public event MethoodContainer ActionCommitted;

    [SerializeField]
    private float _interval;

    [ReadOnly]
    [SerializeField]
    private float _timer = 0;

    private void Start()
    {
        ActionCommitted += PlayerController_ActionCommitted;
    }

    private void PlayerController_ActionCommitted()
    {
        _timer = _interval;
    }

    private void Update()
    {
        if (_timer <= 0)
        {
            _directionOnPlatformController.WhiteMaterial();
            ApproveOfAction();
        }
        else if (_timer > 0)
        {
            _directionOnPlatformController.RedMaterial();
            _timer -= Time.deltaTime;
        }
    }

    public void ApproveOfAction()
    {
        var targetPlatform = _directionOnPlatformController.GetTargetPlatform();
        if (Input.GetMouseButton(_mouseButtonForGenerate.GetHashCode()) && targetPlatform != null
                                        && !targetPlatform.IsEnabled())
        {
            targetPlatform.Enable();
            ActionCommitted();
        }

        if (Input.GetMouseButton(_mouseButtonForShoot.GetHashCode()))
        {
            _gun.Shoot();
            ActionCommitted();
        }
    }
}
