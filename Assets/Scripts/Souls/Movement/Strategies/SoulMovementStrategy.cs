using System;

public abstract class SoulMovementStrategy
{
    protected SoulMovementStrategy(ISoulMovement soulMovement)
    {
        SoulMovement = soulMovement;
    }

    public event Action StrategyWorkFinished;

    public abstract void DoWork();

    protected ISoulMovement SoulMovement { get; }

    protected void OnStrategyWorkFinished()
    {
        StrategyWorkFinished?.Invoke();
    }
}
