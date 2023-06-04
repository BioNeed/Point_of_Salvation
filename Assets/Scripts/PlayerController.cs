using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private CharacterController _charController;
    [SerializeField] private PreDialogue _preDialogue;

    private Vector3 _moveDirection;
    private Quaternion _qRotation;
    private bool _isSoulNearby = false;
    private bool _inDialogue = false;
    private Soul _soulController;

    private void Start()
    {
        _isSoulNearby = false;
        _soulController = null;
    }

    private void Update()
    {
        if (DialogueManager.GetInstance().IsDialoguePlaying)
        {
            _charController.Move(Vector3.zero);
            transform.rotation = _qRotation;
            return;
        }

        _moveDirection = Vector3.zero;
        float axisHor = Input.GetAxis("Horizontal");
        float axisVer = Input.GetAxis("Vertical");
        if (axisHor != 0 || axisVer != 0)
        {
            _moveDirection = new Vector3(axisHor, 0, axisVer) * _speed * Time.deltaTime;
            _qRotation = Quaternion.LookRotation(_moveDirection);
        }
        transform.rotation = _qRotation;
        _charController.Move(_moveDirection);

        if (Input.GetButtonDown("Jump") && _isSoulNearby && _inDialogue == false)
        {
            _inDialogue = true;
            _soulController.DialogIndicatorOff();
            _preDialogue.StartPreDialogue(_soulController);
        }
    }

    public void StopDialogue()
    {
        _inDialogue = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isSoulNearby == false && 
            other.TryGetComponent(out Soul soul) == true && 
            soul.CanTalk == true)
        {
            Debug.Log("OnTriggerEnter");
            _soulController = soul;
            _soulController.DialogIndicatorOn();
            _isSoulNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _soulController?.DialogIndicatorOff();
        _soulController = null;
        _isSoulNearby = false;
    }
}