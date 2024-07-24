using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHungerAction : Action
{
    public override bool Perform(GameObject agent)
    {
        return true;
    }

    public override void Reset()
    {
    }
}
