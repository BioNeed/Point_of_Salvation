using System.Collections;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    private PlayerStates _playerStates = PlayerStates.FreeMovement;

    public bool CanEnterDialogue => _playerStates == PlayerStates.FreeMovement;

    public bool CanMove => _playerStates == PlayerStates.FreeMovement;

    public bool IsInPreDialogue => _playerStates == PlayerStates.InPreDialogue;

    public bool IsInDialogue => _playerStates == PlayerStates.InDialogue;

    public bool IsInMessagePanel => _playerStates == PlayerStates.InMessagePanel;

    protected IEnumerator ChangeStateWithDelay(PlayerStates newPlayerState)
    {
        yield return new WaitForEndOfFrame();
        _playerStates = newPlayerState;
    }

    protected enum PlayerStates
    {
        FreeMovement = 0,
        InPreDialogue = 1,
        InDialogue = 2,
        InJournal = 3,
        InJournalResultPanel = 4,
        InMessagePanel = 5,
    }
}
