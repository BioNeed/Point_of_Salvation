using System;
using UnityEngine;

[RequireComponent(typeof(SoulMovementCalculator))]
[RequireComponent(typeof(SoulMovement))]
[RequireComponent(typeof(SoulStateMachine))]
public class SoulMovementStrategyGenerator : MonoBehaviour
{
    private SoulMovementCalculator _soulMovementCalculator;
    private SoulMovement _soulMovement;
    private SoulStateMachine _soulStateMachine;
    private SoulMovementStrategy _movementStrategy;

    private void Start()
    {
        _soulMovementCalculator = GetComponent<SoulMovementCalculator>();
        _soulMovement = GetComponent<SoulMovement>();
        _soulStateMachine = GetComponent<SoulStateMachine>();
        SetNewMovementStrategy();
    }

    private void Update()
    {
        _movementStrategy.DoWork();
    }

    public void SetStrategyByState(SoulStates state)
    {
        SetNewMovementStrategy(state);
    }

    public void SetStrategy(SoulMovementStrategy newMovementStrategy)
    {
        _movementStrategy.StrategyWorkFinished -= OnStrategyWorkFinished;
        newMovementStrategy.StrategyWorkFinished += OnStrategyWorkFinished;
        _movementStrategy = newMovementStrategy;
        var soulState = GetSoulStateByStrategy(newMovementStrategy);
        _soulStateMachine.SetNewState(soulState);
    }

    private void OnStrategyWorkFinished() =>
        SetNewMovementStrategy();

    private void SetNewMovementStrategy(SoulStates? state = null)
    {
        if (!state.HasValue)
        {
            state = _soulStateMachine.GetNextState();
        }
        else
        {
            _soulStateMachine.SetNewState(state.Value);
        }

        var newSoulMovementStrategy = GetStrategyBySoulState(state.Value);
        if (_movementStrategy is not null)
        {
            _movementStrategy.StrategyWorkFinished -= OnStrategyWorkFinished;
        }

        newSoulMovementStrategy.StrategyWorkFinished += OnStrategyWorkFinished;
        _movementStrategy = newSoulMovementStrategy;
    }

    private SoulMovementStrategy GetStrategyBySoulState(SoulStates state)
    {
        switch (state)
        {
            case SoulStates.Staying:
                {
                    var stayingTimeInSeconds = _soulMovementCalculator.GetStayingTimeInSeconds();
                    return new StayingSoulStrategy(
                        _soulMovement,
                        stayingTimeInSeconds);
                }

            case SoulStates.Moving:
                {
                    var nextMovementPosition = _soulMovementCalculator.GetNextMovementPosition();
                    return new MovingSoulStrategy(
                        _soulMovement,
                        nextMovementPosition);
                }

            case SoulStates.Rotating:
                {
                    var nextRotation = _soulMovementCalculator.GetNextRotation();
                    return new RotatingSoulStrategy(
                        _soulMovement,
                        nextRotation);
                }

            default:
                {
                    throw new InvalidOperationException($"Soul state {state} is not supported");
                }
        }
    }

    private SoulStates GetSoulStateByStrategy(SoulMovementStrategy movementStrategy)
    {
        switch (movementStrategy)
        {
            case MovingSoulStrategy:
                {
                    return SoulStates.Moving;
                }

            case StayingSoulStrategy:
                {
                    return SoulStates.Staying;
                }

            case RotatingSoulStrategy:
                {
                    return SoulStates.Rotating;
                }

            default:
                {
                    throw new InvalidOperationException($"Soul strategy {movementStrategy} is not supported");
                }
        }
    }
}
