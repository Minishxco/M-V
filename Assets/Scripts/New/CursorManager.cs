using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CursorManager : MonoBehaviour
{
    [Header("Cursor Settings")]
    public Texture2D cursorSpriteCharacter1;
    public Texture2D cursorSpriteCharacter2;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    [Header("UI References")]
    [SerializeField] Image cursorImg;
    [SerializeField] float movementSpeed = 2f;
    [SerializeField] GraphicRaycaster graphicRaycaster;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Camera uiCamera;

    bool keyboardMode = false;

    RectTransform canvasRect;

    private void Awake()
    {
        ChangeCursorBasedOnCharacter();

        canvasRect = cursorImg.canvas.GetComponent<RectTransform>();
        cursorImg.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            ToggleInputMode();

        if (!keyboardMode) return;

        MoveCursor();
        UpdateHover();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            TryClickUI();
    }


    private void ChangeCursorBasedOnCharacter()
    {
        int character = UserDataLoader.LoadCharacter();

        if (character == 1 && cursorSpriteCharacter1 != null)
            Cursor.SetCursor(cursorSpriteCharacter1, hotSpot, cursorMode);
        else if (character == 2 && cursorSpriteCharacter2 != null)
            Cursor.SetCursor(cursorSpriteCharacter2, hotSpot, cursorMode);
        else
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    void MoveCursor()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            move += Vector3.up;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            move += Vector3.down;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            move += Vector3.left;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            move += Vector3.right;

        Vector3 newPos = cursorImg.rectTransform.position + move * movementSpeed * Time.deltaTime;

        Vector3 min = canvasRect.TransformPoint(canvasRect.rect.min);
        Vector3 max = canvasRect.TransformPoint(canvasRect.rect.max);

        newPos.x = Mathf.Clamp(newPos.x, min.x, max.x);
        newPos.y = Mathf.Clamp(newPos.y, min.y, max.y);

        cursorImg.rectTransform.position = newPos;
    }

    void UpdateHover()
    {
        PointerEventData pointerData = new PointerEventData(eventSystem);

        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(uiCamera, cursorImg.rectTransform.position);
        pointerData.position = screenPos;

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerData, results);

        if (results.Count > 0)
            eventSystem.SetSelectedGameObject(results[0].gameObject);
        else
            eventSystem.SetSelectedGameObject(null);
    }


    void TryClickUI()
    {
        PointerEventData pointerData = new PointerEventData(eventSystem);

        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(uiCamera, cursorImg.rectTransform.position);
        pointerData.position = screenPos;

        List<RaycastResult> results = new List<RaycastResult>();
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

    void ToggleInputMode()
    {
        keyboardMode = !keyboardMode;

        if (keyboardMode)
            EnableKeyboardCursor();
        else
            EnableMouseCursor();
    }

    void EnableKeyboardCursor()
    {
        Cursor.visible = false;
        cursorImg.gameObject.SetActive(true);

        cursorImg.rectTransform.position = canvasRect.position;
    }

    void EnableMouseCursor()
    {
        Cursor.visible = true;
        cursorImg.gameObject.SetActive(false);
        eventSystem.SetSelectedGameObject(null);
    }
}