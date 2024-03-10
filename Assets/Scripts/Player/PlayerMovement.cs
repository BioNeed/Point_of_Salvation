using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private CharacterController _charController;
    [SerializeField] private PlayerState _playerState;

    private float _fallingSpeed;
    private Vector3 _moveDirection;
    private Quaternion _qRotation;

    private void Update()
    {
        if (!_playerState.CanMove)
        {
            DontMove();
            return;
        }

        _moveDirection = CalculateHorizontalMovement();
        _moveDirection.y = CalculateVerticalMovement();
        transform.rotation = _qRotation;
        _charController.Move(_moveDirection);
    }

    private float CalculateVerticalMovement()
    {
        _fallingSpeed = 0f;
        if (_charController.isGrounded)
        {
            _fallingSpeed = 0f;
        }

        _fallingSpeed += Physics2D.gravity.y * Time.deltaTime;
        return _fallingSpeed;
    }

    private Vector3 CalculateHorizontalMovement()
    {
        var horizontalMovement = Vector3.zero;
        var axisHor = Input.GetAxis("Horizontal");
        var axisVer = Input.GetAxis("Vertical");
        if (axisHor != 0 || axisVer != 0)
        {
            horizontalMovement = new Vector3(axisHor, 0, axisVer) * _speed * Time.deltaTime;
            _qRotation = Quaternion.LookRotation(horizontalMovement, Vector3.up);
        }

        return horizontalMovement;
    }

    private void DontMove()
    {
        _charController.Move(Vector3.zero);
        transform.rotation = _qRotation;
    }
}
