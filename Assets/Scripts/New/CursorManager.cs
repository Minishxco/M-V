using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    [Header("Cursor Settings")]
    public Texture2D cursorSpriteCharacter1;
    public Texture2D cursorSpriteCharacter2;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;


    [SerializeField] Image cursorImg;
    [SerializeField] float movementSpeed = 10;

    bool wasdMode = false;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            ChangeMouseInput();

        if (wasdMode)
            WASD_Movement();
    }

    void WASD_Movement()
    {
        if (Input.GetKey(KeyCode.W))
            cursorImg.rectTransform.position = cursorImg.rectTransform.position + Vector3.up * Time.deltaTime * movementSpeed;
        if (Input.GetKey(KeyCode.A))
            cursorImg.rectTransform.position = cursorImg.rectTransform.position + Vector3.left * Time.deltaTime * movementSpeed;
        if (Input.GetKey(KeyCode.S))
            cursorImg.rectTransform.position = cursorImg.rectTransform.position + Vector3.down * Time.deltaTime * movementSpeed;
        if (Input.GetKey(KeyCode.D))
            cursorImg.rectTransform.position = cursorImg.rectTransform.position + Vector3.right * Time.deltaTime * movementSpeed;
    }

    void ChangeMouseInput()
    {
        wasdMode = !wasdMode;

        if (wasdMode)
            ChangeToWASD();
        else
            ChangeToNormalCursor();
    }
    void ChangeToWASD()
    {
        Cursor.visible = false;
        cursorImg.gameObject.SetActive(true);
        cursorImg.rectTransform.position = Input.mousePosition;
    }

    void ChangeToNormalCursor()
    {
        Cursor.visible = true;
        cursorImg.gameObject.SetActive(false);
    }
}