using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Photon.MonoBehaviour {

    [SerializeField]
    private GameObject _bullet;

    [SerializeField]
    private Transform _positionOfGeneration;

    [SerializeField]
    private float _bulletSpeed;

    private Rigidbody _rigidbody;

    [SerializeField]
    private PhotonView _photonView;

    private Quaternion _selfRotation;

    public void Shoot()
    {
        var instance = Instantiate(_bullet, _positionOfGeneration.position, Quaternion.identity) as UnityEngine.GameObject;
        _rigidbody = instance.GetComponent<Rigidbody>();
        Vector3 direction = Camera.main.transform.TransformDirection(Vector3.forward);
        _rigidbody.AddForce(direction * _bulletSpeed);
    }

    private void Update()
    {
       if (_photonView.isMine)
            transform.rotation = Camera.main.transform.rotation;
       else
            SmoothMovement();
    }

    private void SmoothMovement()
    {
        transform.rotation = _selfRotation;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.rotation);
        }
        else
        {
            _selfRotation = (Quaternion)stream.ReceiveNext();   
        }
    }
}
