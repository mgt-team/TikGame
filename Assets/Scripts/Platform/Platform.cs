using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour
{
    
    [SerializeField]
    private List<Platform> _neighborsList;

    private bool _isEnabled = false;
    
    public void Enable()
    {
        _isEnabled = true;
        gameObject.SetActive(true);
    }
    
    public void Disable()
    {
        _isEnabled = false;
        gameObject.SetActive(false);
    }

    public bool IsEnabled()
    {
        return _isEnabled;
    }

    public bool HasDisabledNeighbor()
    {
        return _neighborsList.Any(neighbor => neighbor.IsEnabled() == false);
    }

    public Platform GetRandomDisabledPlatform()
    {
        var disabledNeighbors = _neighborsList.FindAll(neighbor => neighbor.IsEnabled() == false);

        if (disabledNeighbors.Count == 0)
        {
            Debug.LogWarning("Tries to get disabled platform from platform without disabled neighbor");
            return null;
        }

        return disabledNeighbors[Random.Range(0, disabledNeighbors.Count)];
    }
}
