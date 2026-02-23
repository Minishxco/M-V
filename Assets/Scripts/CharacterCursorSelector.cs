using UnityEngine;

public class CharacterCursorSelector : MonoBehaviour
{
    [Header("Cursor Settings")]
    public Texture2D cursorSpriteCharacter1;
    public Texture2D cursorSpriteCharacter2;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    private void Awake()
    {
        ChangeCursorBasedOnCharacter();
    }

    private void ChangeCursorBasedOnCharacter()
    {
        int character = UserDataLoader.LoadCharacter();

        if (character == 1 && cursorSpriteCharacter1 != null)
        {
            Cursor.SetCursor(cursorSpriteCharacter1, hotSpot, cursorMode);
            Debug.Log("Cursor cambiado al del Personaje 1");
        }
        else if (character == 2 && cursorSpriteCharacter2 != null)
        {
            Cursor.SetCursor(cursorSpriteCharacter2, hotSpot, cursorMode);
            Debug.Log("Cursor cambiado al del Personaje 2");
        }
        else if (character != 1 && character != 2)
        {
            Debug.LogWarning($"Personaje {character} no reconocido. Usando cursor por defecto.");
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
        else
        {
            Debug.LogWarning("No se pudo cambiar el cursor. Verifica los sprites asignados.");
        }
    }
}