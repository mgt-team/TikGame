using UnityEngine;

public class Platform : MonoBehaviour {

    public void Enable()
    {
        gameObject.SetActive(true);
    }
    
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    
}
