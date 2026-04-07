using UnityEngine;

public class SetCursor : MonoBehaviour
{
    public Texture2D systemCursorCharacter1;
    public Texture2D systemCursorCharacter2;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    private void Start()
    {
        ChangeSystemCursorSprite();
    }
    void ChangeSystemCursorSprite()
    {
        int character = UserDataLoader.LoadCharacter();

        if (character == 1 && systemCursorCharacter1 != null)
            Cursor.SetCursor(systemCursorCharacter1, hotSpot, cursorMode);
        else if (character == 2 && systemCursorCharacter2 != null)
            Cursor.SetCursor(systemCursorCharacter2, hotSpot, cursorMode);
        else
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
