using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : MonoBehaviour
{
    [SerializeField] State[] states;
    [SerializeField] Condition[] goalStates;
    public Dictionary<string, float> state { get; private set; } = new Dictionary<string, float>();
    public Dictionary<string, float> goalState { get; private set; } = new Dictionary<string, float>();
    private GPlanner planner;
    private List<Action> currentPlan;
    private int currentActionIndex;
    [SerializeField] int maxcount;

    /// <summary>
    /// Debug用
    /// </summary>
    [SerializeField] Action currentAction;

    private void Awake()
    {
        planner = new GPlanner(maxcount);
        if (states != null)
        {
            foreach (State w in states)
            {
                state.Add(w.key, w.value);
            }
        }

        if (goalStates != null)
        {
            foreach (Condition w in goalStates)
            {
                goalState.Add(w.Key, w.Value);
            }
        }
    }

    void Start()
    {
        // 利用可能なアクションを追加
        planner.Actions = new List<Action>(GetComponents<Action>());
    }

    void Update()
    {
        if (currentPlan == null || currentPlan.Count == 0 || currentActionIndex >= currentPlan.Count)
        {
            // 計画を立てる
            currentPlan = planner.Plan(state, goalState);
            currentActionIndex = 0;
        }

        if (currentPlan != null && currentPlan.Count > 0)
        {
            currentAction = currentPlan[currentActionIndex];
            // 計画を実行する
            ExecutePlan();
        }
    }

    // 計画を実行するメソッド
    void ExecutePlan()
    {
        if (currentActionIndex >= currentPlan.Count)
        {
            return;
        }

        var action = currentPlan[currentActionIndex];

        // 初めての実行時のみPerformメソッドを呼ぶ
        if (action.Status == ActionStatus.Inactive)
        {
            state = action.ApplyPreEffects(state);
            action.Status = ActionStatus.Running;
        }

        action.Perform(gameObject);

        if (action.Status == ActionStatus.Success)
        {
            // アクションが成功したら、次のアクションに移る
            state = action.ApplyPostEffects(state);
            action.Reset();
            currentActionIndex++;
        }
        else if (action.Status == ActionStatus.Failure)
        {
            // アクションが失敗した場合には、新しい計画を立て直す
            currentPlan = planner.Plan(state, goalState);
            currentActionIndex = 0;
        }
    }

    // 状態をJSON形式で保存するメソッド
    public string SaveStateToJson()
    {
        StateContainer stateContainer = new StateContainer(state);
        return JsonUtility.ToJson(stateContainer);
    }

    // JSON形式から状態を読み込むメソッド
    public void LoadStateFromJson(string json)
    {
        StateContainer stateContainer = JsonUtility.FromJson<StateContainer>(json);
        state = stateContainer.ToDictionary();
    }
}