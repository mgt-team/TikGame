using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField]
    private UnityEngine.GameObject _bullet;

    [SerializeField]
    private Transform _positionOfGeneration;

    [SerializeField]
    private UnityEngine.GameObject _trackPoint;

    [SerializeField]
    private float _bulletSpeed;

    private Rigidbody _rigidbody;

    public void Shoot()
    {
        var instance = Instantiate(_bullet, _positionOfGeneration.position, Quaternion.identity) as UnityEngine.GameObject;
        _rigidbody = instance.GetComponent<Rigidbody>();
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        _rigidbody.AddForce(direction * _bulletSpeed);
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
