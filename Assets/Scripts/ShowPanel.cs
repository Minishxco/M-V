using TMPro;
using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject panel, buttonLetter;
    [TextArea(3, 10)]
    public string dialogueText;
    public TextMeshProUGUI TMP_dialogueText;

    private void Awake()
    {
        TMP_dialogueText.text = dialogueText;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        audioManager.PlayOptionsBox();
        buttonLetter.SetActive(true);
        panel.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (panel != null)
        {
            panel.SetActive(false);
            buttonLetter.SetActive(false);
        }
    }
}
