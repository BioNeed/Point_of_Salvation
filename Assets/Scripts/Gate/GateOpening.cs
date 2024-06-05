using UnityEngine;

public class GateOpening : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _finalRotationY;

    private bool _rotating = false;

    private void Update()
    {
        if (_rotating == true)
        {
            var newRotation = new Vector3(0, _finalRotationY, 0);
            transform.eulerAngles = newRotation;
            _rotating = false;
        }
    }

    public void Open()
    {
        _rotating = true;
    }
}
