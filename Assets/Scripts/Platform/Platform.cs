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

    private bool active;

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
        active = true;
    }

    public void DisactivatePlatform()
    {
        active = false;
    }

    private void Update()
    {
        if (!IsEnabled())
        {
            if (active)
            {
                renderer.material = activeGlass;
                if (Input.GetMouseButtonDown(0))
                {
                    Enable();
                }
            }
            else
            {
                renderer.material = glass;
            }
        }
    }
}
