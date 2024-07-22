using System.Collections.Generic;
using UnityEngine;

public class FindBerryBushAction : Action
{
    [SerializeField] float detectionRange = 5;
    public FindBerryBushAction()
    {
        Name = "FindBerryBushAction";
        //PreConditions["food"] = 50.0f;
        //Effects["foundBerryBush"] = 1.0f;
        Cost = 1;
    }

    public override bool Perform(GameObject agent)
    {
        // ベリーの成っている茂みを見つけるロジックを実装
        var berryBush = FindNearestBerryBush();
        if (berryBush == null)
        {
            Debug.Log("berry doesnt exist");
            return false;
        }
        else
        {
            var moveToAction = agent.GetComponent<MoveToBerryBushAction>();
            if (moveToAction != null)
            {
                moveToAction.SetTargetBush(berryBush);
            }

            var pickBerriesAction = agent.GetComponent<PickBerriesAction>();
            if (pickBerriesAction != null)
            {
                pickBerriesAction.SetTargetBush(berryBush);
            }

            if (Vector2.Distance(berryBush.transform.position, transform.position) < detectionRange)
            {
                Debug.Log("Found a berry bush!");
                Status = ActionStatus.Success;
                return true;
            }
            else
            {
                Debug.Log("Search");
                transform.Translate(Quaternion.Euler(0, 0, Time.time) * Vector2.right * Time.deltaTime * 5);
                return false;
            }
        }
    }

    public override void Reset()
    {
        Status = ActionStatus.Inactive;
    }

    private BerryBush FindNearestBerryBush()
    {
        // 近くのベリーの茂みを見つけるためのロジック
        // ここでは簡略化のために最初に見つかった茂みを返すようにしています
        BerryBush[] bushes = FindObjectsOfType<BerryBush>();
        BerryBush nearestBush = null;
        float nearestDist = float.MaxValue;
        foreach (BerryBush bush in bushes)
        {
            if(bush.HasBerries())
            {
                var dist = Vector2.Distance(bush.transform.position, transform.position);
                if(dist < nearestDist)
                {
                    nearestBush = bush;
                    nearestDist = dist;
                }
            }
        }
        return nearestBush;
    }
}
