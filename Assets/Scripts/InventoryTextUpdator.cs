using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTextUpdator : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] ResourceInventory inv;

    void Update()
    {
        var resources = inv.GetResources();
        var newText = "";
        foreach (var resource in resources)
        {
            newText += resource.name + resource.GetAmount() + "\n";
        }
        text.text = newText;
    }
}
