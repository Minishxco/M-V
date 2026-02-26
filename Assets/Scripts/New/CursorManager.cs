using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CursorManager : MonoBehaviour
{
    [Header("Cursor Sprites")]
    public Texture2D systemCursorCharacter1;
    public Texture2D systemCursorCharacter2;
    public Sprite cursorSpriteCharacter1;
    public Sprite cursorSpriteCharacter2;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    [Header("UI References")]
    [SerializeField] Image cursorImg;
    [SerializeField] float movementSpeed = 800f;
    [SerializeField] float acceleration = 2000f;
    [SerializeField] float deceleration = 3000f;
    [SerializeField] GraphicRaycaster graphicRaycaster;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Camera uiCamera;

    RectTransform canvasRect;
    Vector3 currentVelocity;
    bool usingKeyboard = true;

    private void Awake()
    {
        canvasRect = cursorImg.canvas.GetComponent<RectTransform>();
        ChangeCursorSprites();
        EnableKeyboardCursor();
    }

    private void Update()
    {
        DetectInputMethod();
        MoveCursor();
        TryClickUI();
    }

    void DetectInputMethod()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            if (usingKeyboard)
            {
                usingKeyboard = false;
                cursorImg.gameObject.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                ChangeSystemCursorSprite();
            }
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (!usingKeyboard)
            {
                EnableKeyboardCursor();
            }

            usingKeyboard = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            cursorImg.gameObject.SetActive(true);
        }
    }
    void MoveCursor()
    {
        if (!usingKeyboard) return;

        Vector3 inputDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) inputDir += Vector3.up;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) inputDir += Vector3.down;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) inputDir += Vector3.left;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) inputDir += Vector3.right;

        inputDir = inputDir.normalized;

        if (inputDir != Vector3.zero)
        {
            currentVelocity += inputDir * acceleration * Time.deltaTime;
            currentVelocity = Vector3.ClampMagnitude(currentVelocity, movementSpeed);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        Vector3 newPos = cursorImg.rectTransform.position + currentVelocity * Time.deltaTime;

        Vector3 min = canvasRect.TransformPoint(canvasRect.rect.min);
        Vector3 max = canvasRect.TransformPoint(canvasRect.rect.max);

        newPos.x = Mathf.Clamp(newPos.x, min.x, max.x);
        newPos.y = Mathf.Clamp(newPos.y, min.y, max.y);

        cursorImg.rectTransform.position = newPos;
    }

    void TryClickUI()
    {
        if (!usingKeyboard) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            List<RaycastResult> results = new List<RaycastResult>();
            PointerEventData pointerData = new PointerEventData(eventSystem);
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(uiCamera, cursorImg.rectTransform.position);
            pointerData.position = screenPos;

            graphicRaycaster.Raycast(pointerData, results);

            foreach (RaycastResult result in results)
            {
                Button button = result.gameObject.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.Invoke();
                    break;
                }
            }
        }
    }

    void EnableKeyboardCursor()
    {
        cursorImg.gameObject.SetActive(true);
        currentVelocity = Vector3.zero;

        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvasRect,
            mousePos,
            cursorImg.canvas.worldCamera,
            out worldPos
        );
        cursorImg.rectTransform.position = worldPos;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void ChangeCursorSprites()
    {
        int character = UserDataLoader.LoadCharacter();

        if (character == 1 && cursorSpriteCharacter1 != null)
            cursorImg.sprite = cursorSpriteCharacter1;
        else if (character == 2 && cursorSpriteCharacter2 != null)
            cursorImg.sprite = cursorSpriteCharacter2;

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

    public Image GetCursorImage()
    {
        return cursorImg;
    }
}