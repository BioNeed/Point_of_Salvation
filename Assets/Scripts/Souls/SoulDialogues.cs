using UnityEngine;

public class SoulDialogues : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset _jsonDialogue;
    [SerializeField] private TextAsset _jsonPreDialogue;

    public string DialogueJsonText => _jsonDialogue.text;

    public string PreDialogueJsonText => _jsonPreDialogue.text;
}
