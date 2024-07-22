using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateContainer
{
    public List<StateEntry> entries = new List<StateEntry>();

    public StateContainer(Dictionary<string, float> state)
    {
        foreach (var kvp in state)
        {
            entries.Add(new StateEntry(kvp.Key, kvp.Value));
        }
    }

    public Dictionary<string, float> ToDictionary()
    {
        var dictionary = new Dictionary<string, float>();
        foreach (var entry in entries)
        {
            dictionary[entry.key] = entry.value;
        }
        return dictionary;
    }
}

[Serializable]
public class StateEntry
{
    public string key;
    public float value;

    public StateEntry(string key, float value)
    {
        this.key = key;
        this.value = value;
    }
}
