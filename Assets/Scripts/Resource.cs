using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "New Resource")]
public class Resource : ScriptableObject
{
    [SerializeField] int _amount;
    [SerializeField] Sprite _sprite;
    [SerializeField] string _description;
    [SerializeField] ResourceInventory inv;

    public int GetAmount() { return _amount; }

    public void AddAmount(int amount)
    {
        inv.AddResource(this);
        _amount += amount;
    }

    public bool ConsumeAmount(int amount)
    {
        var aaa = _amount < amount;
        if (aaa)
        {
            _amount -= amount;
        }
        return aaa;
    }
}
