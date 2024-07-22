using System;
using System.Collections.Generic;
using UnityEngine;

public class GPlanner
{
    public List<Action> Actions;

    public List<Action> Plan(Dictionary<string, float> initialState, Dictionary<string, float> goalState)
    {
        var openList = new List<Node>();
        var closedList = new HashSet<Node>();
        var startNode = new Node(null, 0.0f, initialState, null);
        openList.Add(startNode);

        var count = 0;
        while (openList.Count > 0)
        {
            openList.Sort((node1, node2) => node1.Cost.CompareTo(node2.Cost));
            var currentNode = openList[0];
            openList.RemoveAt(0);

            if (IsGoalAchieved(currentNode.State, goalState))
            {
                return BuildPlan(currentNode);
            }

            closedList.Add(currentNode);

            foreach (var action in Actions)
            {
                if (action.ArePreConditionsMet(currentNode.State))
                {
                    // コストが0の場合はスキップ
                    if (action.Cost <= 0)
                    {
                        Debug.LogAssertion("コストが0以下のActionは使用できません");
                        continue;
                    }

                    var newState = action.ApplyPreEffects(currentNode.State);
                    newState = action.ApplyPostEffects(currentNode.State);
                    var newNode = new Node(currentNode, currentNode.Cost + action.Cost, newState, action);

                    if (!closedList.Contains(newNode) && !openList.Exists(n => n.Equals(newNode)))
                    {
                        openList.Add(newNode);
                    }
                }
            }
            if(count > 100)
            {
                Debug.LogAssertion("detect infinity loop");
                break;
            }

            count++;
        }
        return null;
    }

    private bool IsGoalAchieved(Dictionary<string, float> state, Dictionary<string, float> goalState)
    {
        foreach (var goal in goalState)
        {
            if (!state.ContainsKey(goal.Key) || state[goal.Key] < goal.Value)
            {
                return false;

            }
        }
        return true;
    }

    private List<Action> BuildPlan(Node node)
    {
        var plan = new List<Action>();
        while (node.Parent != null)
        {
            plan.Insert(0, node.Action);
            node = node.Parent;
        }
        return plan;
    }
}


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

