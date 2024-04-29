using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SoulMovement : MonoBehaviour, ISoulMovement
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private float _fallingSpeed;
    private CharacterController _charController;

    private void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    public void DontMove()
    {
        _charController.Move(Vector3.zero);
    }

    public void Move(Vector3 finalPosition)
    {
        var moveDirection = CalculateHorizontalMovement(finalPosition);
        moveDirection.y = CalculateVerticalMovement();
        _charController.Move(moveDirection);
    }

    public void Rotate(Quaternion finalRotation)
    {
        var maxDegreesDelta = _rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, finalRotation, maxDegreesDelta);
    }

    private Vector3 CalculateHorizontalMovement(Vector3 finalPosition)
    {
        var rawMovement = new Vector3
        {
            x = finalPosition.x - transform.position.x,
            z = finalPosition.z - transform.position.z,
        };

        var clampedMovement = Vector3.ClampMagnitude(rawMovement, Vector3.forward.magnitude);
        return clampedMovement * _speed * Time.deltaTime;
    }

    private float CalculateVerticalMovement()
    {
        if (_charController.isGrounded)
        {
            _fallingSpeed = 0f;
        }

        _fallingSpeed += Physics2D.gravity.y * Time.deltaTime;
        return _fallingSpeed;
    }
}
