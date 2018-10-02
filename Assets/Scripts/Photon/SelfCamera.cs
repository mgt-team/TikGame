using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfCamera : Photon.MonoBehaviour {

    [SerializeField]
    private PhotonView _photonView;

    private Quaternion _selfRotation;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!_photonView.isMine)
            gameObject.SetActive(false);
    }
}
