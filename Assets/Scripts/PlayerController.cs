using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private CharacterController _charController;

    private Vector3 _moveDirection;
    private Quaternion _qRotation;
    private int _soulLayer;
    private bool _isSoulNearby;
    private SoulController _soulController;
    private TextAsset _inkJSON;

    private void Start()
    {
        _isSoulNearby = false;
        _soulController = null;
        _soulLayer = LayerMask.NameToLayer("Soul");
    }

    private void Update()
    {
        if (DialogueManager.GetInstance().IsDialoguePlaying || DialogueManager.GetInstance().IsJournalOpen)
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

        if (Input.GetButtonDown("Jump") && _isSoulNearby)
        {
            _soulController.DialogIndicatorOff();
            _inkJSON = _soulController.GetInkFile();
            DialogueManager.GetInstance().EnterDialogueMode(_soulController, _inkJSON);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isSoulNearby || other.gameObject.layer == _soulLayer)
        {
            _soulController = other.GetComponent<SoulController>();
            _soulController.DialogIndicatorOn();
            _isSoulNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _soulController.DialogIndicatorOff();
        _soulController = null;
        _isSoulNearby = false;
    }
    
}
