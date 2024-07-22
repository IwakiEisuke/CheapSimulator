using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStateText : MonoBehaviour
{
    [SerializeField] CharacterAI character;
    [SerializeField] Text textMesh;

    private void Reset()
    {
        textMesh = GetComponent<Text>();
    }

    void Update()
    {
        var newText = "";
        foreach(var state in character.state)
        {
            newText += state.ToString() + "\n";
        }

        textMesh.text = newText;
    }
}
