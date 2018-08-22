using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private FinishPlatform finishPlatform;

    private void Start()
    {
        finishPlatform.onFinish += FinishPlatform_onFinish;
    }

    private void FinishPlatform_onFinish()
    {
        Debug.Log("Finish");
    }
}
