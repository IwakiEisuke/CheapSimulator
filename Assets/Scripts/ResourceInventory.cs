using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Inventory/New ResourceInventory")]
public class ResourceInventory : ScriptableObject
{
    [SerializeField] List<Resource> _resources;

    public void AddResource(Resource resource)
    {
        if (!_resources.Contains(resource))
        {
            _resources.Add(resource);
        }
    }

    public List<Resource> GetResources()
    {
        return _resources;
    }
}
