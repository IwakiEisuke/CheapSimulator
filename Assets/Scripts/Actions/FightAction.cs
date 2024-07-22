
// 戦うアクションのクラス
using UnityEngine;

public class FightAction : Action
{
    public FightAction()
    {
        Name = "Fight";
        //PreConditions["health"] = 50.0f;  // 健康状態が50以上であれば実行可能
        //Effects["enemyDefeated"] = 1.0f;  // アクション実行後、敵が倒される
        //Effects["safe"] = 1.0f;
        Cost = 2.0f;                      // アクションのコスト
    }

    public override bool Perform(GameObject agent)
    {
        // 戦うロジックを実装
        Debug.Log("Fighting the enemy!");
        return true;
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
