using UnityEngine;

public class BerryBush : MonoBehaviour
{
    public int berries = 5; // 初期状態のベリーの数
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Sprite sp;

    public bool PickBerries(int amount)
    {
        if (berries >= amount)
        {
            berries -= amount;
            if(berries == 0)
            {
                rend.sprite = sp;
            }
            return true;
        }
        return false;
    }

    public bool HasBerries()
    {
        return berries > 0;
    }
}
