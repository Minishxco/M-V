using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSelector : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public VideoClip videoPersonaje1;
    public VideoClip videoPersonaje2;

    public GameObject video1;
    public GameObject panelSeleccionar;
    public GameObject panelNombre;

    private string escenaSiguiente = "ESCENA 5";

    private bool personajeSeleccionado = false;

    void Awake()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    public void StartVideo()
    {
        panelNombre.SetActive(false);
        video1.SetActive(true);
    }

    // BOTÓN PERSONAJE 1
    public void PlayPersonaje1()
    {
        personajeSeleccionado = true;
        PlayVideo(videoPersonaje1);
    }

    // BOTÓN PERSONAJE 2
    public void PlayPersonaje2()
    {
        personajeSeleccionado = true;
        PlayVideo(videoPersonaje2);
    }

    void PlayVideo(VideoClip clip)
    {
        video1.SetActive(true);
        panelSeleccionar.SetActive(false);

        videoPlayer.clip = clip;
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        if (personajeSeleccionado)
        {
            SceneManager.LoadScene(escenaSiguiente);
        }
        else
        {
            video1.SetActive(false);
            panelSeleccionar.SetActive(true);
        }
    }
}
