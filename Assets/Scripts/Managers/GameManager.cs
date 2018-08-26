using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private FinishPlatform _finishPlatform;
    
    [SerializeField]
    private PlatformManager _platformManager;

    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private DirectionOnPlatformController _directionOnPlatformController;

    [SerializeField]
    private float _interval;

    [ReadOnly]
    [SerializeField]
    private float _timer = 0;

    private void Start()
    {
        _finishPlatform.onFinish += FinishPlatform_onFinish;
        _playerController.ActionCommitted += _playerController_ActionCommitted;
        _platformManager.InitPlatformMap();
    }

    private void _playerController_ActionCommitted()
    {
        _timer = _interval;
    }

    private void FinishPlatform_onFinish()
    {
        Debug.Log("Finish");
    }

    private void Update()
    {
        if (_timer <= 0)
        {
            _directionOnPlatformController.WhiteMaterial();
            _playerController.ApproveOfAction();
        }
        else if (_timer > 0)
        {
            _directionOnPlatformController.RedMaterial();
            _timer -= Time.deltaTime;
        }
    }
}
