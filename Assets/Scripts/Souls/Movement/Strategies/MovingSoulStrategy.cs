using UnityEngine;

public class MovingSoulStrategy: SoulMovementStrategy
{
    private const float EpsilonDistance = 0.1f;
    private Vector3 _finalPosition;

    public MovingSoulStrategy(
        ISoulMovement soulMovement,
        Vector3 finalPosition) : base(soulMovement)
    {
        _finalPosition = finalPosition;
    }

    public override void DoWork()
    {
        SoulMovement.Move(_finalPosition);

        var movementLeft = new Vector3
        {
            x = SoulMovement.transform.position.x - _finalPosition.x,
            z = SoulMovement.transform.position.z - _finalPosition.z,
        };
        
        if (movementLeft.magnitude <= EpsilonDistance)
        {
            OnStrategyWorkFinished();
        }
    }
}
