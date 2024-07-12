using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] float _add, _mag;
    [SerializeField] Transform parent;

    public void Ammm(ref float current)
    {
        current *= _mag;
        Destroy(gameObject.GetComponent<Button>());
        gameObject.transform.SetParent(parent);
    }

    public void BuyUpgrade()
    {
        ScoreManager.BuyUpgrade(this);
    }
}
