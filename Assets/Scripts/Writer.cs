using System.Collections;
using UnityEngine;
using TMPro;

public class Writer : MonoBehaviour
{
    public float delay = 0.05f;
    public float delayBetweenTexts = 0.3f;
    public bool hasName, hasBotton;
    public GameObject closeButton;

    public TextMeshProUGUI[] texts;
    public AudioManager audioManager;
    private bool isActive;


    private void Awake()
    {
        if (hasName)
        {
            texts[0].text = UserDataLoader.LoadName() + " " + texts[0].text;
        }
    }

    private void OnEnable()
    {
        ResetTexts();
        StartCoroutine(TypeAll());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        if(audioManager != null )
        {
            audioManager.StopKeyboard();
        }        
    }

    void ResetTexts()
    {
        foreach (TextMeshProUGUI tmp in texts)
        {
            tmp.ForceMeshUpdate();
            tmp.maxVisibleCharacters = 0;
        }
    }

    IEnumerator TypeAll()
    {
        if (audioManager != null)
        {
            audioManager.PlayKeyboard();
        }

        foreach (TextMeshProUGUI tmp in texts)
        {
            tmp.ForceMeshUpdate();
            int totalCharacters = tmp.textInfo.characterCount;

            for (int i = 0; i <= totalCharacters; i++)
            {
                tmp.maxVisibleCharacters = i;
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitForSeconds(delayBetweenTexts);
        }

        if(hasBotton)
        {
            closeButton.SetActive(true);
        }

        if (audioManager != null)
        {
            audioManager.StopKeyboard();
        }
    }

}
