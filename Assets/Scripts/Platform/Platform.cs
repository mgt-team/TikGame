using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Platform : Photon.PunBehaviour
{
    
    [SerializeField]
    private List<Platform> _neighborsList;

    [SerializeField]
    private List<Transform> _sidePointList;

    [SerializeField]
    private Material _disableMaterial;

    [SerializeField]
    private Material _enableMaterial;

    [SerializeField]
    private PhotonView _photonView;

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

    public Transform GetTransform()
    {
        return gameObject.transform;
    }
    
    private void Awake()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _collider = gameObject.GetComponent<Collider>();
        _photonView = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void Enable()
    {
        _isEnabled = true;
        _renderer.material = _enableMaterial;
        _collider.isTrigger = false;
    }
    
    [PunRPC]
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

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            bool isTrigger = _collider.isTrigger;
            //Color color = _renderer.material.color;
            stream.SendNext(isTrigger);
            //stream.SendNext(color);
        }
        else
        {
            _collider.isTrigger = (bool)stream.ReceiveNext();
            //_renderer.material.color = (Color)stream.ReceiveNext();
        }
    }
}
