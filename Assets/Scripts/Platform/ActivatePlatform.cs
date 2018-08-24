using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatform : MonoBehaviour {

    [SerializeField]
    private Platform platform;

    private RaycastHit hit;
    private Transform transform;

    public Camera camera;

    private void Start()
    {
        transform = gameObject.transform;
    }

    private void Update()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(transform.position, direction, out hit, 1000f))          //Регистрация столкновения луча с объектом 
        {
            if (hit.transform.tag == "Platform")                                    //Проверка объекта
            {
                if (platform != hit.transform.GetComponent<Platform>())
                {
                    if(platform != null)                                            //Смена активной платформы
                        platform.DisactivatePlatform();
                    platform = hit.transform.GetComponent<Platform>();
                    platform.ActivatePlatform();
                }
            }
        }
        else
        {
            if (platform != null)
            {
                platform.DisactivatePlatform();
                platform = null;
            }
        }
    }
}
