using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CarLevelController : MonoBehaviour
{
    public Animator carAnimator;
    public PlayVideo playVideo;
    public GameObject fadeObject;
    public Animator fadeAnimator;

    public float correctAnimDuration = 2f;
    public float delayBeforeFade = 1f;

    public float fadeDuration = 1f;

    public void RespuestaCorrecta()
    {
        carAnimator.SetBool("Correct", true);
        carAnimator.SetBool("Incorrect", false);

        StartCoroutine(SecuenciaCorrecta());
    }

    public void RespuestaIncorrecta()
    {
        carAnimator.SetBool("Correct", false);
        carAnimator.SetBool("Incorrect", true);
    }

    IEnumerator SecuenciaCorrecta()
    {
        yield return new WaitForSeconds(correctAnimDuration);

        yield return new WaitForSeconds(delayBeforeFade);

        fadeObject.SetActive(true);

        yield return new WaitForSeconds(fadeDuration);

        playVideo.PlayVideoFromButton();
    }

    public void ResetEstado()
    {
        carAnimator.SetBool("Correct", false);
        carAnimator.SetBool("Incorrect", false);
    }
}