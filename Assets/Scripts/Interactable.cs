using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

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

    public bool herramientas_ = true;

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

    public string tagKey;
    public GameObject outlineKey;
    public GameObject ButtonKey;

    public string sceneName;
    public bool goToSceneOnFinish = true;
    public float waitTimeBeforeScene = 0.5f;

    private void Start()
    {
        if(herramientas_)
        {
            foreach (Herramienta h in herramientas)
            {
                h.imagenButton.GetComponent<CircleCollider2D>().enabled = false;
            }
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

        if (other.CompareTag(tagKey))
        {
            outlineKey.SetActive(true);
            ButtonKey.SetActive(true);
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

        if (other.CompareTag(tagKey))
        {
            outlineKey.SetActive(false);
            ButtonKey.SetActive(false);
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
        llave.SetActive(false);
        keyManager.updateKey();
        Debug.Log("Llave final");

        if (goToSceneOnFinish)
        {
            StartCoroutine(LoadSceneAfterDelay(sceneName));
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

    public void RespuestaCorrecta()
    {
        audioManager.PlayCorrectAnswer();

        nivel = 2;
        panelCorrecto.SetActive(true);

        foreach (var panel in dialoguePanel)
        {
            panel.SetActive(false);
        }

        if(herramientas_)
        {
            foreach (Herramienta h in herramientas)
            {
                h.imagenButton.GetComponent<SpriteRenderer>().enabled = true;
                h.imagenButton.GetComponent<CircleCollider2D>().enabled = true;
            }
        }
        
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

        foreach (var panel in dialoguePanel)
        {
            panel.SetActive(false);
        }

        nivel = 1;
        panelIncorrecto.SetActive(true);
    }

    public void CerrarCorrecto()
    {
        foreach (var imgLetters in imageLetters)
        {
            imgLetters.SetActive(false);
        }
        panelCorrecto.SetActive(false);
        if(imageLettersCorrecto != null)
        {
            imageLettersCorrecto.SetActive(true);
        }
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

    IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(waitTimeBeforeScene);
        SceneSelector.Instance.CompleteCurrentMission();

        //SceneManager.LoadScene(sceneName);
    }
}
