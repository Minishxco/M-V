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

    [Header("Mouse Switch")]
    [SerializeField] float mouseSwitchDistancePx = 50f;
    [SerializeField] int mouseSwitchStableFrames = 2;

    RectTransform canvasRect;
    Vector3 currentVelocity;

    bool usingKeyboard = true;

    Vector2 lastMousePos;
    int mouseMovedFrames;

    readonly List<RaycastResult> raycastResults = new List<RaycastResult>(32);
    PointerEventData pointerData;

    void Awake()
    {
        if (cursorImg == null)
        {
            Debug.LogError("CursorManager: cursorImg no asignado.");
            enabled = false;
            return;
        }

        if (eventSystem == null) eventSystem = EventSystem.current;
        if (eventSystem == null)
        {
            Debug.LogError("CursorManager: EventSystem no asignado y no existe EventSystem en escena.");
            enabled = false;
            return;
        }

        if (graphicRaycaster == null) graphicRaycaster = cursorImg.canvas.GetComponent<GraphicRaycaster>();
        if (graphicRaycaster == null)
        {
            Debug.LogError("CursorManager: GraphicRaycaster no asignado y no se encontr  en el Canvas.");
            enabled = false;
            return;
        }

        if (uiCamera == null) uiCamera = cursorImg.canvas.worldCamera;

        canvasRect = cursorImg.canvas.GetComponent<RectTransform>();
        pointerData = new PointerEventData(eventSystem);

        ChangeCursorSprites();
        EnableKeyboardCursor();
    }

    void Update()
    {
        DetectInputMethod();
        MoveCursor();
        TryClickUI();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) return;
        if (!usingKeyboard) ChangeSystemCursorSprite();
    }

    void OnApplicationPause(bool pause)
    {
        if (pause) return;
        if (!usingKeyboard) ChangeSystemCursorSprite();
    }

    void DetectInputMethod()
    {
        bool anyMoveKey =
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

        if (anyMoveKey)
        {
            if (!usingKeyboard) EnableKeyboardCursor();

            usingKeyboard = true;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
            cursorImg.gameObject.SetActive(true);

            lastMousePos = Input.mousePosition;
            mouseMovedFrames = 0;
            return;
        }

        if (usingKeyboard)
        {
            Vector2 cur = (Vector2)Input.mousePosition;
            float dist = Vector2.Distance(cur, lastMousePos);

            if (dist >= mouseSwitchDistancePx)
                mouseMovedFrames++;
            else
                mouseMovedFrames = 0;

            if (mouseMovedFrames >= mouseSwitchStableFrames)
            {
                SwitchToMouse();
                lastMousePos = cur;
                mouseMovedFrames = 0;
            }
        }
    }

    void SwitchToMouse()
    {
        usingKeyboard = false;
        cursorImg.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ChangeSystemCursorSprite();
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
            raycastResults.Clear();

            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(uiCamera, cursorImg.rectTransform.position);
            pointerData.position = screenPos;

            graphicRaycaster.Raycast(pointerData, raycastResults);

            for (int i = 0; i < raycastResults.Count; i++)
            {
                var btn = raycastResults[i].gameObject.GetComponentInParent<Button>();
                if (btn != null)
                {
                    btn.onClick.Invoke();
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
        Cursor.lockState = CursorLockMode.None;

        lastMousePos = Input.mousePosition;
        mouseMovedFrames = 0;
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

    public Image GetCursorImage() => cursorImg;
}