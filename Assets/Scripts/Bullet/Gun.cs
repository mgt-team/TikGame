using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Photon.MonoBehaviour {

    [SerializeField]
    private UnityEngine.GameObject _bullet;

    [SerializeField]
    private Transform _positionOfGeneration;

    [SerializeField]
    private UnityEngine.GameObject _trackPoint;

    [SerializeField]
    private float _bulletSpeed;

    private Rigidbody _rigidbody;

    [PunRPC]
    public void Shoot()
    {
        var instance = Instantiate(_bullet, _positionOfGeneration.position, Quaternion.identity) as UnityEngine.GameObject;
        _rigidbody = instance.GetComponent<Rigidbody>();
        Vector3 direction = Camera.main.transform.TransformDirection(Vector3.forward);
        _rigidbody.AddForce(direction * _bulletSpeed);
    }

    [PunRPC]
    private void FixedUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
