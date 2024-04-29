using UnityEngine;

public class RotatingSoulStrategy : SoulMovementStrategy
{
    private readonly Quaternion _finalRotation;

    public RotatingSoulStrategy(
        ISoulMovement soulMovement,
        Quaternion finalRotation) : base(soulMovement)
    {
        _finalRotation = finalRotation;
    }

    public override void DoWork()
    {
        SoulMovement.Rotate(_finalRotation);
        if (SoulMovement.transform.rotation == _finalRotation)
        {
            OnStrategyWorkFinished();
        }
    }
}
