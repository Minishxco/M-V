using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseHoverPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject panel;

    [Header("Hover Settings")]
    private float extraHoverRange = 15f;

    private RectTransform rectTransform;
    private CursorManager cursorManager;
    private bool isHoveringVirtual = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        cursorManager = FindFirstObjectByType<CursorManager>();

        if (panel != null)
            panel.SetActive(false);
    }

    private void Update()
    {
        if (cursorManager == null) return;

        if (!cursorManager.GetCursorImage().gameObject.activeSelf)
        {
            if (isHoveringVirtual)
            {
                isHoveringVirtual = false;
                HidePanel();
            }
            return;
        }

        Image cursorImg = cursorManager.GetCursorImage();

        Rect expandedRect = rectTransform.rect;
        expandedRect.xMin -= extraHoverRange;
        expandedRect.xMax += extraHoverRange;
        expandedRect.yMin -= extraHoverRange;
        expandedRect.yMax += extraHoverRange;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            cursorImg.rectTransform.position,
            null,
            out localPoint
        );

        bool over = expandedRect.Contains(localPoint);

        if (over && !isHoveringVirtual)
        {
            isHoveringVirtual = true;
            ShowPanel();
        }
        else if (!over && isHoveringVirtual)
        {
            isHoveringVirtual = false;
            HidePanel();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowPanel();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HidePanel();
    }

    void ShowPanel()
    {
        if (panel != null)
            panel.SetActive(true);
    }

    void HidePanel()
    {
        if (panel != null)
            panel.SetActive(false);
    }
}