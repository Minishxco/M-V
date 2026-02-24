using UnityEngine;

public class ResponseManager : MonoBehaviour
{
    public GameObject panelIncorrecto1, panelIncorrecto2, panelIncorrecto3;
    public GameObject ButtonA, ButtonB, ButtonC;
    public GameObject panelCorrecto;
    public GameObject panelA, panelB, panelC, panelD;
    public AudioManager audioManager;


    public void RespuestaIncorrecta1()
    {
        panelA.SetActive(false);
        RespuestaIncorrecta(panelIncorrecto1);
    }

    public void RespuestaIncorrecta2()
    {
        panelB.SetActive(false);
        RespuestaIncorrecta(panelIncorrecto2);
    }

    public void RespuestaIncorrecta3()
    {
        panelC.SetActive(false);
        RespuestaIncorrecta(panelIncorrecto3);
    }

    public void RespuestaCorrecta()
    {
        audioManager.PlayCorrectAnswer();
        panelA.SetActive(false);
        panelB.SetActive(false);
        panelC.SetActive(false);
        panelD.SetActive(false);

        ButtonB.SetActive(false);
        ButtonC.SetActive(false);
        ButtonA.SetActive(false);

        panelCorrecto.SetActive(true);
    }

    private void RespuestaIncorrecta(GameObject panelIncorrecto)
    {
        if (audioManager != null)
        {
            audioManager.PlayWrongAnswer();
        }
        else
        {
            Debug.LogWarning("No se pudo reproducir sonido: AudioManager es null");
        }

        panelIncorrecto.SetActive(true);
    }

    public void CerrarCorrecto()
    {
        panelCorrecto.SetActive(false);
    }

}
