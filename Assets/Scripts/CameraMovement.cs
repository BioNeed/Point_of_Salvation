using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3 _offset;
    private Vector3 _offsetY = new Vector3(0f, 0.5f, 0f);

    private void Start()
    {
        _offset = _target.position - transform.position;
    }

    private void LateUpdate()
    {
        transform.position = _target.position - _offset;
        transform.LookAt(_target.position + _offsetY);
    }
}
