using System.Linq;
using UnityEngine;

public class SoulMovementCalculator : MonoBehaviour
{
    private const int FullPercentage = 100;

    [SerializeField] private int _minMovementToWayPointAsPercentage;
    [SerializeField] private int _maxMovementToWayPointAsPercentage;
    [SerializeField] private int _minStayingInSeconds;
    [SerializeField] private int _maxStayingInSeconds;
    [SerializeField] private Transform _wayPointsParent;

    private Transform[] _wayPoints;
    private int _nextWayPointIndex;

    private void Awake()
    {
        _nextWayPointIndex = 0;
        _wayPoints = _wayPointsParent
            .GetComponentsInChildren<SoulWaypoint>()
            .Select(w => w.transform)
            .ToArray();
    }

    public Vector3 GetNextMovementPosition()
    {
        var currentPosition = transform.position;
        var nextWayPointPosition = _wayPoints[_nextWayPointIndex].position;
        var fullMovement = new Vector3
        {
            x = nextWayPointPosition.x - currentPosition.x,
            z = nextWayPointPosition.z - currentPosition.z,
        };

        var movementPercentage = Random.Range(
            _minMovementToWayPointAsPercentage, 
            _maxMovementToWayPointAsPercentage);

        return currentPosition + fullMovement * movementPercentage / FullPercentage;
    }

    public Quaternion GetNextRotation()
    {
        _nextWayPointIndex = GetRandomNumberExcludingValue(
            _nextWayPointIndex,
            0,
            _wayPoints.Length - 1);

        var nextPosition = _wayPoints[_nextWayPointIndex].position;
        var direction = nextPosition - transform.position;
        return Quaternion.LookRotation(direction, Vector3.up);
    }

    public float GetStayingTimeInSeconds()
    {
        return Random.Range(_minStayingInSeconds, _maxStayingInSeconds);
    }

    private int GetRandomNumberExcludingValue(
        int excludingValue,
        int minInclusive,
        int maxInclusive)
    {
        int generatedValue;
        do
        {
            generatedValue = Random.Range(minInclusive, maxInclusive);
        } while (generatedValue == excludingValue);

        return generatedValue;
    }
}
