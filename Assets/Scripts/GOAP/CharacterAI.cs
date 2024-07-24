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
    /// Debug�p
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
        // ���p�\�ȃA�N�V������ǉ�
        planner.Actions = new List<Action>(GetComponents<Action>());
    }

    void Update()
    {
        if (currentPlan == null || currentPlan.Count == 0 || currentActionIndex >= currentPlan.Count)
        {
            // �v��𗧂Ă�
            currentPlan = planner.Plan(state, goalState);
            currentActionIndex = 0;
        }

        if (currentPlan != null && currentPlan.Count > 0)
        {
            currentAction = currentPlan[currentActionIndex];
            // �v������s����
            ExecutePlan();
        }
    }

    // �v������s���郁�\�b�h
    void ExecutePlan()
    {
        if (currentActionIndex >= currentPlan.Count)
        {
            return;
        }

        var action = currentPlan[currentActionIndex];

        // ���߂Ă̎��s���̂�Perform���\�b�h���Ă�
        if (action.Status == ActionStatus.Inactive)
        {
            state = action.ApplyPreEffects(state);
            action.Status = ActionStatus.Running;
        }

        action.Perform(gameObject);

        if (action.Status == ActionStatus.Success)
        {
            // �A�N�V����������������A���̃A�N�V�����Ɉڂ�
            state = action.ApplyPostEffects(state);
            action.Reset();
            currentActionIndex++;
        }
        else if (action.Status == ActionStatus.Failure)
        {
            // �A�N�V���������s�����ꍇ�ɂ́A�V�����v��𗧂Ē���
            currentPlan = planner.Plan(state, goalState);
            currentActionIndex = 0;
        }
    }

    // ��Ԃ�JSON�`���ŕۑ����郁�\�b�h
    public string SaveStateToJson()
    {
        StateContainer stateContainer = new StateContainer(state);
        return JsonUtility.ToJson(stateContainer);
    }

    // JSON�`�������Ԃ�ǂݍ��ރ��\�b�h
    public void LoadStateFromJson(string json)
    {
        StateContainer stateContainer = JsonUtility.FromJson<StateContainer>(json);
        state = stateContainer.ToDictionary();
    }
}