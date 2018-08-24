using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour
{
    
    [SerializeField]
    private List<Platform> _neighborsList;

    [SerializeField]
    private Material activeGlass;

    [SerializeField]
    private Material glass;

    [SerializeField]
    private Material platform;

    private bool _isEnabled = false;

    private Renderer renderer;
    private BoxCollider collider;

    private void Awake()
    {
        renderer = gameObject.GetComponent<Renderer>();
        collider = gameObject.GetComponent<BoxCollider>();

    }

    public void Enable()
    {
        _isEnabled = true;
        renderer.material = platform;
        collider.isTrigger = false;
    }
    
    public void Disable()
    {
        _isEnabled = false;
        renderer.material = glass;
        collider.isTrigger = true;
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

    public void ActivatePlatform()
    {
        if (!IsEnabled())
        {
            renderer.material = activeGlass;
        }
    }

    public void DisactivatePlatform()
    {
        if (!IsEnabled())
        {
            renderer.material = glass;
        }
    }
}
