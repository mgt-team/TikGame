using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour
{
    
    [SerializeField]
    private List<Platform> _neighborsList;

    [SerializeField]
    private List<Transform> _sidePointList;

    [SerializeField]
    private Material _disableMaterial;

    [SerializeField]
    private Material _enableMaterial;

    private bool _isEnabled = false;

    private Renderer _renderer;
    private Collider _collider;

    public IEnumerable<Transform> GetSidePointList()
    {
        return _sidePointList;
    }

    public void SetNeighbors(List<Platform> neighborsList)
    {
        if (neighborsList.Count > _sidePointList.Count)
        {
            Debug.LogError("Platform has more neighbors than sides");
        }
        
        _neighborsList = neighborsList;
    }
    
    private void Awake()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _collider = gameObject.GetComponent<Collider>();

    }

    public void Enable()
    {
        _isEnabled = true;
        _renderer.material = _enableMaterial;
        _collider.isTrigger = false;
    }
    
    public void Disable()
    {
        _isEnabled = false;
        _renderer.material = _disableMaterial;
        _collider.isTrigger = true;
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
    
    /**
     * Called when mouse directed on this platform
     */
    public void OnDirected(Material directedMaterial)
    {
        if (!IsEnabled())
        {
            _renderer.material = directedMaterial;
        }
    }

    public void OnOutFromDirection()
    {
        if (!IsEnabled())
        {
            _renderer.material = _disableMaterial;
        }
    }
    
    protected void OnValidate()
    {
        _sidePointList = GetComponentsInChildren<Transform>().ToList();
        _sidePointList.Remove(transform);
    }
}
