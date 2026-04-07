using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeEntryUIDelay : MonoBehaviour
{
    [SerializeField] Image back;
    [SerializeField] float delay;
    float targetAlpha = 0.8f;
    float fadeDuration = 1f;

    void Start()
    {
        SetAlpha(0f);
        StartCoroutine(DelayForAnimEntry());
    }

    IEnumerator DelayForAnimEntry()
    {
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, targetAlpha, elapsed / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(targetAlpha);
    }

    void SetAlpha(float alpha)
    {
        Color c = back.color;
        c.a = alpha;
        back.color = c;
    }
}