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
