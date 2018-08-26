using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private FinishPlatform _finishPlatform;
    
    [SerializeField]
    private PlatformManager _platformManager;

    [SerializeField]
    private Gun _gun;

    [SerializeField]
    private DirectionOnPlatformController direction;

    public float interval;
    public float timer;

    private void Start()
    {
        _finishPlatform.onFinish += FinishPlatform_onFinish;
        
        _platformManager.InitPlatformMap();
    }

    private void FinishPlatform_onFinish()
    {
        Debug.Log("Finish");
    }
}
