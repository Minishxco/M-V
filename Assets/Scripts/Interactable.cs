using UnityEngine;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{
    [System.Serializable]
    public class Herramienta
    {
        public string tag;                 // Tag de la herramienta
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

    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (Herramienta h in herramientas)
        {
            if (other.CompareTag(h.tag))
            {
                herramientaActual = h;
                Debug.Log("Tocó: " + h.tag);

                if (nivel == 1 && h.panelNivel1 != null)
                    h.panelNivel1.SetActive(true);
                else if (nivel == 2 && h.panelNivel2 != null)
                    h.panelNivel2.SetActive(true);

                break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (herramientaActual == null) return;

        if (other.CompareTag(herramientaActual.tag))
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

        herramientaActual.spriteMostrar.SetActive(true);
        herramientaActual.imagenButton.SetActive(false);

        VerificarSpritesActivos();
    }

    public void llaveFinal()
    {
        llave.SetActive(false);
        Debug.Log("Juego Terminado");
    }

    public void RespuestaCorrecta()
    {
   
        if (herramientaActual != null && herramientaActual.panelNivel1 != null)
        {
            herramientaActual.panelNivel1.SetActive(false);
        }

        nivel = 2;
        panelCorrecto.SetActive(true);
        
        foreach (Herramienta h in herramientas)
        {
            h.imagenButton.GetComponent<SpriteRenderer>().enabled = true;
            h.imageLetters.SetActive(false);
        }
    }

    public void RespuestaIncorrecta()
    {

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
