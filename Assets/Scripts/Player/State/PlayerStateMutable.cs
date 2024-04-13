public class PlayerStateMutable : PlayerState
{
    public void FreePlayer()
    {
        StartCoroutine(ChangeStateWithDelay(PlayerStates.FreeMovement));
        //_playerStates = PlayerStates.FreeMovement;
    }

    public void EnterPreDialogue()
    {
        StartCoroutine(ChangeStateWithDelay(PlayerStates.InPreDialogue));
        //_playerStates = PlayerStates.InDialogue;
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
}