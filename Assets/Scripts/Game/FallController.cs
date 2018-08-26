using System;
using UnityEngine;

public class FallController : MonoBehaviour {
    
    private void OnCollisionEnter(Collision other)
    {
        if (TagManager.CompareCollisionTag(other, TagEnum.Player))
        {
            OnPlayerFall(other.gameObject);
        }

        if (TagManager.CompareCollisionTag(other, TagEnum.Bullet))
        {
            OnBulletFall(other.gameObject);
        }
    }

    private void OnPlayerFall(GameObject player)
    {
        
    }

    private void OnBulletFall(GameObject bullet)
    {
        Destroy(bullet);
    }
}
