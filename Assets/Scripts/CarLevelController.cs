using UnityEngine;

public class CarLevelController : MonoBehaviour
{
    public Animator carAnimator;
    public void RespuestaCorrecta()
    {
        carAnimator.SetBool("Correct", true);
        carAnimator.SetBool("Incorrect", false);
    }
    public void RespuestaIncorrecta()
    {
        carAnimator.SetBool("Correct", false);
        carAnimator.SetBool("Incorrect", true);
    }

    public void ResetEstado()
    {
        carAnimator.SetBool("Correct", false);
        carAnimator.SetBool("Incorrect", false);
    }
}
