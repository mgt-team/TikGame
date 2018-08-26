using UnityEngine;

public class FallController : MonoBehaviour {
    
    private void OnCollisionEnter(Collision other)
    {
        if (TagManager.CompareCollisionTag(other, TagEnum.Player))
        {
            OnPlayerFell(other.gameObject.GetComponent<Player>());
        }
        else if (TagManager.CompareCollisionTag(other, TagEnum.Bullet))
        {
            OnBulletFell(other.gameObject.GetComponent<Bullet>());
        }
    }

    private void OnBulletFell(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        Destroy(bullet);
    }

    private void OnPlayerFell(Player player)
    {
        
    }
    
    
}
