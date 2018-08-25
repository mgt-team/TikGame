using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour {

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform positionOfGeneration;

    [SerializeField]
    private GameObject trackPoint;

    public float speedBullet;

    private Rigidbody _rigidbody;

    public void Shoot()
    {
        var instance = Instantiate(bullet, positionOfGeneration.position, Quaternion.identity) as GameObject;
        _rigidbody = instance.GetComponent<Rigidbody>();
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        _rigidbody.AddForce(direction * speedBullet);
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
