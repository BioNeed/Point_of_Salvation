using UnityEngine;

public class GateEntering : MonoBehaviour
{
    private bool _hasKey = false;

    public bool HasKey => _hasKey;

    public void FoundKey()
    {
        _hasKey = true;
    }
}
