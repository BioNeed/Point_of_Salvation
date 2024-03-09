using UnityEditor;
using UnityEngine;

public class CursorVisibility : MonoBehaviour
{
    [SerializeField] private bool _visibleCursor;

    private void Start()
    {
        Cursor.visible = _visibleCursor;
        Cursor.SetCursor(PlayerSettings.defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
