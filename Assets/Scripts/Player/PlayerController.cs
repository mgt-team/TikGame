using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerController : Photon.MonoBehaviour
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
    public event MethoodContainer ShootCommitted;
    public event MethoodContainer GenerateCommitted;

    [SerializeField]
    private float _shootCooldown;

    [SerializeField]
    private float _shootTimer = 0;

    [SerializeField]
    private float _generateCooldown;

    [SerializeField]
    private float _generateTimer = 0;

    [SerializeField]
    private PhotonView _photonView;

    private void Start()
    {
        if (!_photonView.isMine)
            enabled = false;
        ShootCommitted += PlayerController_ShootCommitted;
        GenerateCommitted += PlayerController_GenerateCommitted;
    }

    private void PlayerController_GenerateCommitted()
    {
        _generateTimer = _generateCooldown;
    }

    private void PlayerController_ShootCommitted()
    {
        _shootTimer = _shootCooldown;
    }

    private void Update()
    {
        if (_generateTimer <= 0)
        {
            _directionOnPlatformController.YellowMaterial();
            GenerateApprove();
        }
        else if (_generateTimer > 0)
        {
            _directionOnPlatformController.RedMaterial();
            _generateTimer -= Time.deltaTime;
        }

        if(_shootTimer <= 0)
        {
            ShootApprove();
        }
        else if(_shootTimer > 0)
        {
            _shootTimer -= Time.deltaTime;
        }
    }

    public void ShootApprove()
    {
        if (Input.GetMouseButton(_shootMouseButton.GetHashCode()))
        {
            _gun.Shoot();
            ShootCommitted();
        }
    }

    public void GenerateApprove()
    {
        var targetPlatform = _directionOnPlatformController.GetTargetPlatform();
        if (Input.GetMouseButton(_generateMouseButton.GetHashCode()) && targetPlatform != null
                                        && !targetPlatform.IsEnabled())
        {
            targetPlatform.Enable();
            GenerateCommitted();
        }
    }
}
