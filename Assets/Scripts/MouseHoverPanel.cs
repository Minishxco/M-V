using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHoverPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject panel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (panel != null)
            panel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (panel != null)
            panel.SetActive(false);
    }
}
