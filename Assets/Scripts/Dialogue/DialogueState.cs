using UnityEngine;

public class DialogueState : MonoBehaviour
{
    public bool IsDialoguePlaying { get; private set; } = false;

    public void EnterDialogue()
    {
        IsDialoguePlaying = true;
    }

    public void FinishDialogue()
    {
        IsDialoguePlaying = false;
    }
}