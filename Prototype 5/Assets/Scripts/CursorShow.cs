#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class CursorShow : MonoBehaviour
{
    #if UNITY_EDITOR
    void Start()
    {
        Debug.Log("ִ��CursorShow�ű�");
        Cursor.visible = true;
        Cursor.SetCursor(PlayerSettings.defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    #endif
}
