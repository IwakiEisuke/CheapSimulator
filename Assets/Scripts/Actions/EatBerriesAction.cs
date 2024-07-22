using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBerriesAction : Action
{
    public override bool Perform(GameObject agent)
    {
        Debug.Log("Eat");
        Status = ActionStatus.Success;
        return true;
    }

    public override void Reset()
    {
        Status = ActionStatus.Inactive;
    }
}
