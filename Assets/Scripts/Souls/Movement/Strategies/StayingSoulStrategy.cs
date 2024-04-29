using UnityEngine;

public class StayingSoulStrategy : SoulMovementStrategy
{
    private float _timeInSecondsToStay;

    public StayingSoulStrategy(
        ISoulMovement soulMovement,
        float timeInSecondsToStay) : base(soulMovement)
    {
        _timeInSecondsToStay = timeInSecondsToStay;
    }

    public override void DoWork()
    {
        SoulMovement.DontMove();
        _timeInSecondsToStay -= Time.deltaTime;
        if (_timeInSecondsToStay < 0)
        {
            OnStrategyWorkFinished();
        }
    }
}