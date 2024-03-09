using UnityEngine;

public class DialogueEntering : MonoBehaviour
{
    [SerializeField] private PreDialogue _preDialogue;
    [SerializeField] private DialogueState _dialogueState;

    private Soul _nearbySoul = null;

    private void Update()
    {
        if (Input.GetButtonDown("Jump") 
                && _nearbySoul != null 
                && !_dialogueState.IsDialoguePlaying)
        {
            _nearbySoul.DialogIndicatorOff();
            _preDialogue.StartPreDialogue(_nearbySoul);
            _dialogueState.EnterDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_nearbySoul == null && 
            other.TryGetComponent(out Soul soul) == true && 
            soul.CanTalk == true)
        {
            _nearbySoul = soul;
            _nearbySoul.DialogIndicatorOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_nearbySoul is not null)
        {
            _nearbySoul.DialogIndicatorOff();
        }

        _nearbySoul = null;
    }
}