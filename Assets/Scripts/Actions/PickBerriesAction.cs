using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBerriesAction : Action
{
    private float t;
    private BerryBush targetBush;

    public PickBerriesAction()
    {
        Name = this.GetType().Name[..^6];
        Cost = 1;
    }

    public void SetTargetBush(BerryBush bush)
    {
        targetBush = bush;
    }

    public override bool Perform(GameObject agent)
    {
        if (targetBush == null)
        {
            Status = ActionStatus.Failure;
            Debug.Log("Bush is null");
            return false;
        }

        if (!targetBush.HasBerries())
        {
            Status = ActionStatus.Failure;
            Debug.Log("No berries left in the bush!");
            return false;
        }
        
        if (2 < (t += Time.deltaTime))
        {
            targetBush.PickBerries(1);
            Debug.Log("Picked berries!");
            t = 0;
            Status = ActionStatus.Success;
            return true;
        }
        return false;
    }

    public override void Reset()
    {
        Status = ActionStatus.Inactive;
    }
}
