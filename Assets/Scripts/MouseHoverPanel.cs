using UnityEngine;
using UnityEngine.UI;

public class MouseHoverPanel : MonoBehaviour
{
    public GameObject panel;

    private Image buttonImage;
    private RectTransform rectTransform;
    private bool isHovering = false;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();

        if (panel != null)
            panel.SetActive(false);
    }

    private void Update()
    {
        CursorManager cursorManager = FindObjectOfType<CursorManager>();

        if (cursorManager != null)
        {
            Image cursorImg = cursorManager.GetComponent<CursorManager>().GetCursorImage();

            if (cursorImg != null)
            {
                bool mouseOverButton = RectTransformUtility.RectangleContainsScreenPoint(
                    rectTransform,
                    cursorImg.rectTransform.position,
                    null
                );

                if (mouseOverButton && !isHovering)
                {
                    isHovering = true;
                    OnPointerEnter();
                }

                else if (!mouseOverButton && isHovering)
                {
                    isHovering = false;
                    OnPointerExit();
                }
            }
        }
    }

    private void OnPointerEnter()
    {
        if (panel != null)
        {
            panel.SetActive(true);
            Debug.Log("Panel activado: " + gameObject.name);
        }
    }

    private void OnPointerExit()
    {
        if (panel != null)
        {
            panel.SetActive(false);
            Debug.Log("Panel desactivado: " + gameObject.name);
        }
    }
}