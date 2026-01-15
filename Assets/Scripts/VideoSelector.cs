using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

public class VideoSelector : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    // RUTAS RELATIVAS DENTRO DE StreamingAssets
    private string videoPersonaje1 = "Video/VideoPersonaje1.mp4";
    private string videoPersonaje2 = "Video/VideoPersonaje2.mp4";

    public GameObject video1;
    public GameObject panelSeleccionar;
    public GameObject panelNombre;

    private bool personajeSeleccionado = false;

    void Awake()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.source = VideoSource.Url;
    }

    void Start()
    {
        if (UserDataLoader.LoadGame() == 1)
        {
            SceneManager.LoadScene(1);
        }
    }

    void Update()
    {
        if (videoPlayer.isPlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            FinishVideo();
        }
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

    void PlayVideo(string videoPath)
    {
        video1.SetActive(true);
        panelSeleccionar.SetActive(false);

        videoPlayer.url = Application.streamingAssetsPath + "/" + videoPath;


        StartCoroutine(PlayPrepared());
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        if (personajeSeleccionado)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            video1.SetActive(false);
            panelSeleccionar.SetActive(true);
        }
    }

    public void FinishVideo()
    {
        OnVideoFinished(videoPlayer);
    }

    IEnumerator PlayPrepared()
    {
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayer.Play();
    }
}
