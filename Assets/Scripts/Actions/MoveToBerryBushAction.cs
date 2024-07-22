using UnityEngine;

public class MoveToBerryBushAction : Action
{
    private BerryBush targetBush;
    public override bool Perform(GameObject agent)
    {
        var dir = (targetBush.transform.position - transform.position).normalized;
        transform.Translate(dir * Time.deltaTime * 5);
        if (Vector2.Distance(targetBush.transform.position, transform.position) < 1)
        {
            Debug.Log("Reach");
            Status = ActionStatus.Success;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void Reset()
    {
        targetBush = null;
        Status = ActionStatus.Inactive;
    }

    public void SetTargetBush(BerryBush bush)
    {
        targetBush = bush;
    }
}
