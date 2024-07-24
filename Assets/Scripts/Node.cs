using System.Collections.Generic;

public class Node
{
    public Node Parent;
    public float Cost;
    public Dictionary<string, float> State;
    public Action Action;

    public Node(Node parent, float cost, Dictionary<string, float> state, Action action)
    {
        Parent = parent;
        Cost = cost;
        State = state;
        Action = action;
    }

    public override bool Equals(object obj)
    {
        if (obj is Node node)
        {
            foreach (var key in State.Keys)
            {
                if (!node.State.ContainsKey(key) || node.State[key] != State[key])
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    public override int GetHashCode()
    {
        int hash = 13;
        foreach (var key in State.Keys)
        {
            hash = (hash * 7) + State[key].GetHashCode();
        }
        return hash;
    }
}
