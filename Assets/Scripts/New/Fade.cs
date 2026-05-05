using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] Image fadeImg;
    [SerializeField] Image textImg;

    [SerializeField] Color[] fadeColor;
    [SerializeField] Sprite[] textSprite;

    private void Start()
    {
        int index = MissionsDone.Instance.GetMissionsDone();
        fadeImg.color = fadeColor[index];
        textImg.sprite = textSprite[index];
    }
}
