using UnityEngine;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{
    [System.Serializable]
    public class Herramienta
    {
        public string tagHerramienta;                 // Tag de la herramienta
        public GameObject panelNivel1;     // Panel inicial
        public GameObject panelNivel2;     // Selector o panel nivel 2
        public GameObject imagenButton;        // Imagen UI
        public GameObject spriteMostrar; // Sprite que se muetra al final
        public GameObject imageLetters;
    }

    [Header("Herramientas")]
    public List<Herramienta> herramientas = new List<Herramienta>();

    [Header("Panels de Respuesta")]
    public GameObject panelCorrecto;
    public GameObject imageLettersCorrecto;
    public GameObject panelIncorrecto;

    public GameObject llave;

    private int nivel = 1;
    private Herramienta herramientaActual;

    public AudioManager audioManager;

    private void Start()
    {
        foreach (Herramienta h in herramientas)
        {
            h.imagenButton.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (Herramienta h in herramientas)
        {
            if (other.CompareTag(h.tagHerramienta))
            {
                herramientaActual = h;
                Debug.Log("Tocó: " + h.tagHerramienta);

                if (nivel == 1 && h.panelNivel1 != null)
                {
                    DesactivarTodosPanelesNivel1();
                    audioManager.PlayOptionsBox();
                    h.panelNivel1.SetActive(true);
                }
                else if (nivel == 2 && h.panelNivel2 != null)
                {
                    h.panelNivel2.SetActive(true);
                }  
                break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (herramientaActual == null) return;

        if (other.CompareTag(herramientaActual.tagHerramienta))
        {
            if (nivel == 1 && herramientaActual.panelNivel1 != null)
                herramientaActual.panelNivel1.SetActive(false);
            else if (nivel == 2 && herramientaActual.panelNivel2 != null)
                herramientaActual.panelNivel2.SetActive(false);

            herramientaActual = null;
        }
    }

    public void SeleccionarObjeto()
    {
        if (herramientaActual == null) return;
        audioManager.PlaySelectTools();
        herramientaActual.spriteMostrar.SetActive(true);
        herramientaActual.imagenButton.SetActive(false);

        VerificarSpritesActivos();
    }

    public void llaveFinal()
    {
        audioManager.PlayKeyCollected();
        llave.SetActive(false);
        Debug.Log("Juego Terminado");
    }

    void DesactivarTodosPanelesNivel1()
    {
        foreach (Herramienta h in herramientas)
        {
            if (h.panelNivel1 != null && h.panelNivel1.activeSelf)
            {
                h.panelNivel1.SetActive(false);
            }
        }
    }


    public void RespuestaCorrecta()
    {
        audioManager.PlayCorrectAnswer();

        if (herramientaActual != null && herramientaActual.panelNivel1 != null)
        {
            herramientaActual.panelNivel1.SetActive(false);
        }

        nivel = 2;
        panelCorrecto.SetActive(true);
        
        foreach (Herramienta h in herramientas)
        {
            h.imagenButton.GetComponent<SpriteRenderer>().enabled = true;
            h.imagenButton.GetComponent<CircleCollider2D>().enabled = true;
            h.imageLetters.SetActive(false);
        }
    }

    public void RespuestaIncorrecta()
    {
        audioManager.PlayWrongAnswer();

        if (herramientaActual != null && herramientaActual.panelNivel1 != null)
        {
            herramientaActual.panelNivel1.SetActive(false);
        }

        nivel = 1;
        panelIncorrecto.SetActive(true);
    }

    public void CerrarCorrecto()
    {
        panelCorrecto.SetActive(false);
        imageLettersCorrecto.SetActive(true);
    }

    public void VerificarSpritesActivos()
    {
        foreach (Herramienta h in herramientas)
        {
            if (h.spriteMostrar == null || !h.spriteMostrar.activeSelf)
            {
                return;
            }
        }

        llave.SetActive(true);
    }
}
