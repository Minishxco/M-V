using UnityEngine;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{
    [System.Serializable]
    public class Herramienta
    {
        public string tagHerramienta;                 // Tag de la herramienta
        public GameObject panelNivel2;     // Selector o panel nivel 2
        public GameObject imagenButton;        // Imagen UI
        public GameObject spriteMostrar; // Sprite que se muetra al final
    }

    [Header("Herramientas")]
    public List<Herramienta> herramientas = new List<Herramienta>();

    [Header("Panels de Respuesta")]
    public GameObject panelCorrecto;
    public GameObject imageLettersCorrecto;
    public GameObject panelIncorrecto1, panelIncorrecto2, panelIncorrecto3;

    public GameObject llave;

    public GameObject[] dialoguePanel;

    private int nivel = 1;
    private Herramienta herramientaActual;

    public AudioManager audioManager;

    public KeyManager keyManager;
    public GameObject[] imageLetters;

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

                if (nivel == 2 && h.panelNivel2 != null)
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
            if (nivel == 2 && herramientaActual.panelNivel2 != null)
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
        keyManager.updateKey();
        Debug.Log("Juego Terminado");
    }


    public void RespuestaCorrecta()
    {
        audioManager.PlayCorrectAnswer();

        nivel = 2;
        panelCorrecto.SetActive(true);

        foreach (Herramienta h in herramientas)
        {
            h.imagenButton.GetComponent<SpriteRenderer>().enabled = true;
            h.imagenButton.GetComponent<CircleCollider2D>().enabled = true;
        }

        foreach (var imgLetters in imageLetters)
        {
            imgLetters.SetActive(false);
        }
    }

    public void RespuestaIncorrecta1()
    {
        RespuestaIncorrecta(panelIncorrecto1);
    }

    public void RespuestaIncorrecta2()
    {
        RespuestaIncorrecta(panelIncorrecto2);
    }

    public void RespuestaIncorrecta3()
    {
        RespuestaIncorrecta(panelIncorrecto3);
    }

    private void RespuestaIncorrecta(GameObject panelIncorrecto)
    {
        audioManager.PlayWrongAnswer();

        foreach (var panel in dialoguePanel)
        {
            panel.SetActive(false);
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
