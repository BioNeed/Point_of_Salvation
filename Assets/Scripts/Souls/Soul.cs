using UnityEngine;

[RequireComponent(typeof(SoulDialogueIndicator))]
[RequireComponent(typeof(SoulPortraitInfo))]
[RequireComponent(typeof(SoulColorChanging))]
[RequireComponent(typeof(SoulFacts))]
[RequireComponent(typeof(SoulDialogues))]
public class Soul : MonoBehaviour
{
    [SerializeField] private bool _canTalk = true;

    public bool CanTalk => _canTalk;

    public SoulDialogueIndicator DialogueIndicator { get; private set; }

    public SoulPortraitInfo PortraitInfo { get; private set; }

    public SoulFacts Facts { get; private set; }

    public SoulDialogues Dialogues { get; private set; }

    private void Awake()
    {
        DialogueIndicator = GetComponent<SoulDialogueIndicator>();
        PortraitInfo = GetComponent<SoulPortraitInfo>();
        Facts = GetComponent<SoulFacts>();
        Dialogues = GetComponent<SoulDialogues>();
    }

    public void DisableTalking()
    {
        _canTalk = false;
    }
}
