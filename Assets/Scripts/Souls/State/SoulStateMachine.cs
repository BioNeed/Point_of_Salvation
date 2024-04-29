using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoulStateMachine : MonoBehaviour
{
    private readonly Dictionary<SoulStates, int> _defaultStatesToWeights
        = new Dictionary<SoulStates, int>
        {
            { SoulStates.Rotating, 1 },
        };

    private SoulState _currentState;

    public SoulStates GetNextState()
    {
        var nextStatesToWeights = _currentState is null
            ? _defaultStatesToWeights
            : _currentState.NextPossibleStatesToPossibilityWeights;

        var nextState = GetNextStateByWeightedRandom(nextStatesToWeights);
        _currentState = nextState;
        return nextState.State;
    }

    public void SetNewState(SoulStates soulState)
    {
        _currentState = soulState.GetSoulStateByEnumState();
    }

    private SoulState GetNextStateByWeightedRandom(Dictionary<SoulStates, int> nextStatesToWeights)
    {
        var totalWeight = nextStatesToWeights.Values.Sum();

        var randomNumber = UnityEngine.Random.Range(1, totalWeight);

        var valuesSum = 0;
        foreach (var nextStateToWeight in nextStatesToWeights)
        {
            valuesSum += nextStateToWeight.Value;

            if (valuesSum >= randomNumber)
            {
                return nextStateToWeight.Key.GetSoulStateByEnumState();
            }
        }

        throw new InvalidOperationException("Cannot randomize next soul state");
    }
}
