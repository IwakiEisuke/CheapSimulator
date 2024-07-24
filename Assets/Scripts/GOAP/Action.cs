using System.Collections.Generic;
using UnityEngine;

public enum ConditionType
{
    GreaterThanOrEqual,
    LessThanOrEqual,
    GreaterThan,
    LessThan,
    Equal
}

public enum EffectType
{
    Set,
    Add,
    Multiply,
    Divide
}

[System.Serializable]
public class Condition
{
    public string Key;
    public float Value;
    public ConditionType ConditionType;
}

[System.Serializable]
public class Effect
{
    public string Key;
    public float Value;
    public EffectType EffectType;
}

public enum ActionStatus
{
    Inactive,
    Running,
    Success,
    Failure
}

public abstract class Action : MonoBehaviour
{
    public ActionStatus Status { get; set; } = ActionStatus.Inactive;
    public string Name;
    public List<Condition> PreConditions;
    public List<Effect> PreEffects;
    public List<Effect> Effects;
    public float Cost;

    public abstract void Reset();
    public abstract bool Perform(GameObject agent);

    public bool ArePreConditionsMet(Dictionary<string, float> state)
    {
        foreach (var precondition in PreConditions)
        {
            if (!state.ContainsKey(precondition.Key)) //このActionに到達するまでに前提条件のstateを設定されていなければ失敗
            {
                return false;
            }

            float stateValue = state[precondition.Key];
            switch (precondition.ConditionType)
            {
                case ConditionType.GreaterThanOrEqual:
                    if (stateValue < precondition.Value) return false;
                    break;
                case ConditionType.LessThanOrEqual:
                    if (stateValue > precondition.Value) return false;
                    break;
                case ConditionType.GreaterThan:
                    if (stateValue <= precondition.Value) return false;
                    break;
                case ConditionType.LessThan:
                    if (stateValue >= precondition.Value) return false;
                    break;
                case ConditionType.Equal:
                    if (stateValue != precondition.Value) return false;
                    break;
            }
        }
        return true;
    }

    public Dictionary<string, float> ApplyPreEffects(Dictionary<string, float> state)
    {
        return ApplyEffects(state, PreEffects);
    }

    public Dictionary<string, float> ApplyPostEffects(Dictionary<string, float> state)
    {
        return ApplyEffects(state, Effects);
    }

    private Dictionary<string, float> ApplyEffects(Dictionary<string, float> state, List<Effect> effects)
    {
        var newState = new Dictionary<string, float>(state);
        foreach (var effect in effects)
        {
            if (newState.ContainsKey(effect.Key))
            {
                switch (effect.EffectType)
                {
                    case EffectType.Set:
                        newState[effect.Key] = effect.Value;
                        break;
                    case EffectType.Add:
                        newState[effect.Key] += effect.Value;
                        break;
                    case EffectType.Multiply:
                        newState[effect.Key] *= effect.Value;
                        break;
                    case EffectType.Divide:
                        newState[effect.Key] /= effect.Value;
                        break;
                }
            }
            else
            {
                newState[effect.Key] = effect.Value;
            }
        }
        return newState;
    }
}
