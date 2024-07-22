// 逃げるアクションのクラス
using UnityEngine;

public class RunAwayAction : Action
{
    public RunAwayAction()
    {
        Name = "Run Away";
        //PreConditions["health"] = 0.0f;  // 健康状態が0以上であれば実行可能
        //Effects["safe"] = 1.0f;          // アクション実行後、安全になる
        Cost = 1.0f;                     // アクションのコスト
    }

    public override bool Perform(GameObject agent)
    {
        // 逃げるロジックを実装
        Debug.Log("Running away!");
        return true;
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}