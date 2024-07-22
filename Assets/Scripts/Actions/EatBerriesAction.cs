using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBerriesAction : Action
{
    public override bool Perform(GameObject agent)
    {
        Debug.Log("Eat");
        Debug.Log(GetComponent<CharacterAI>().state["food"]);
        Status = ActionStatus.Success;
        return true;
    }

    public override void Reset()
    {
        Status = ActionStatus.Inactive;
    }
}
