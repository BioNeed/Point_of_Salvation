using UnityEngine;

public class CurrentSoulContext : MonoBehaviour
{
    public Soul CurrentSoul { get; private set; }

    public void SetCurrentSoul(Soul soul)
    {
        CurrentSoul = soul;
    }
}
