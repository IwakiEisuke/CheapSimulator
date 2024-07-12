using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.UI;

public class SpinButtonController : MonoBehaviour
{
    [SerializeField] InputField amount;

    public void Increase()
    {
        var max = "";
        for (int i = 0; i < amount.characterLimit; i++)
        {
            max += 9;
        }
        var n = int.TryParse(amount.text, out int result) ? result : 0;
        n = Mathf.Clamp(n + 1, 0, int.Parse(max));
        amount.text = n.ToString();
    }

    public void Decrease()
    {
        var max = "";
        for (int i = 0; i < amount.characterLimit; i++)
        {
            max += 9;
        }
        var n = int.Parse(amount.text);
        n = Mathf.Clamp(n - 1, 0, int.Parse(max));
        amount.text = n.ToString();
    }
}
