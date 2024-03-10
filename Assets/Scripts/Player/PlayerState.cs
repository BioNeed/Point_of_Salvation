using System.Collections;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private PlayerStates _playerStates = PlayerStates.FreeMovement;

    public bool CanEnterDialogue => _playerStates == PlayerStates.FreeMovement;

    public bool CanMove => _playerStates == PlayerStates.FreeMovement;

    public bool IsInDialogue => _playerStates == PlayerStates.InDialogue;

    public bool IsInMessagePanel => _playerStates == PlayerStates.InMessagePanel;

    public void FreePlayer()
    {
        StartCoroutine(ChangeStateWithDelay(PlayerStates.FreeMovement));
        //_playerStates = PlayerStates.FreeMovement;
    }
    
    public void EnterDialogue()
    {
        StartCoroutine(ChangeStateWithDelay(PlayerStates.InDialogue));
        //_playerStates = PlayerStates.InDialogue;
    }

    public void EnterJournal()
    {
        StartCoroutine(ChangeStateWithDelay(PlayerStates.InJournal));
        //_playerStates = PlayerStates.InJournal;
    }

    public void EnterJournalResultPanel()
    {
        StartCoroutine(ChangeStateWithDelay(PlayerStates.InJournalResultPanel));
        //_playerStates = PlayerStates.InJournalResultPanel;
    }

    public void EnterMessagePanel()
    {
        StartCoroutine(ChangeStateWithDelay(PlayerStates.InMessagePanel));
        //_playerStates = PlayerStates.InMessagePanel;
    }

    private IEnumerator ChangeStateWithDelay(PlayerStates newPlayerState)
    {
        yield return new WaitForEndOfFrame();
        _playerStates = newPlayerState;
    }

    private enum PlayerStates
    {
        FreeMovement = 0,
        InDialogue = 1,
        InJournal = 2,
        InJournalResultPanel = 3,
        InMessagePanel = 4,
    }
}
