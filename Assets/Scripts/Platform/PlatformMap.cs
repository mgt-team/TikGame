using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformMap : MonoBehaviour
{

    [SerializeField] 
    private List<Platform> _platforms;

    public List<Platform> GetPlatforms()
    {
        return _platforms;
    }

    private void OnValidate()
    {
        _platforms = GetComponentsInChildren<Platform>().ToList();
    }
}
