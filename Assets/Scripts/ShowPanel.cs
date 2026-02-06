using TMPro;
using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject panel;
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
        panel.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
}
