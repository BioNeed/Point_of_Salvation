using UnityEngine;

public class DialogueEntering : MonoBehaviour
{
    [SerializeField] private PreDialogue _preDialogue;
    [SerializeField] private PlayerState _playerState;

    private Soul _nearbySoul = null;

    private void Update()
    {
        if (Input.GetButtonDown("Jump")
                && _nearbySoul != null
                && _playerState.CanEnterDialogue)
        {
            _nearbySoul.DialogueIndicator.TurnOff();
            _preDialogue.StartPreDialogue(_nearbySoul);
            _nearbySoul = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Soul soul) == true 
                && _nearbySoul == null
                && soul.CanTalk == true)
        {
            _nearbySoul = soul;
            _nearbySoul.DialogueIndicator.TurnOn();
            StopSoulMovement(soul);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Soul soul) == true)
        {
            if (_nearbySoul is not null && soul.CanTalk)
            {
                _nearbySoul.DialogueIndicator.TurnOff();
                _nearbySoul = null;
            }

            RotateSoul(soul);
        }
    }

    private static void RotateSoul(Soul soul)
    {
        var soulMovementStrategyGenerator = soul.GetComponent<SoulMovementStrategyGenerator>();
        soulMovementStrategyGenerator.SetStrategyByState(SoulStates.Rotating);
    }

    private static void StopSoulMovement(Soul soul)
    {
        var soulMovementStrategyGenerator = soul.GetComponent<SoulMovementStrategyGenerator>();
        var soulMovement = soul.GetComponent<SoulMovement>();
        var newMovementStrategy = new StayingSoulStrategy(soulMovement, float.MaxValue);
        soulMovementStrategyGenerator.SetStrategy(newMovementStrategy);
    }
}
