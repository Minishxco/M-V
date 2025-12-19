using System.Collections;
using UnityEngine;
using TMPro;

public class Writer : MonoBehaviour
{
    public float delay = 0.05f;
    public float delayBetweenTexts = 0.3f;

    public TextMeshProUGUI[] texts;

    private void OnEnable()
    {
        ResetTexts();
        StartCoroutine(TypeAll());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
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
    }
}
