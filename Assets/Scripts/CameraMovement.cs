using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private float _initialY;
    private Vector3 _offset;
    private Vector3 _offsetY = new Vector3(0f, 0.5f, 0f);

    private void Start()
    {
        _offset = _target.position - transform.position;
        _initialY = transform.position.y;
    }

    private void LateUpdate()
    {
        var newPosition = _target.position - _offset;
        newPosition.y = _initialY;
        transform.position = newPosition;
        transform.LookAt(_target.position + _offsetY);
    }
}
