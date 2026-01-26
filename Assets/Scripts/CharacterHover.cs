using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterHover : MonoBehaviour, IPointerEnterHandler
{
    public CharacterSelector selector;
    public int characterID = 1; // 1 o 2

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (characterID == 1)
            selector.SelectCharacter1();
        else
            selector.SelectCharacter2();
    }
}
