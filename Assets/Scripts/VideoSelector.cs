using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

public class VideoSelector : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoPlayer videoPlayerPreload;

    public string videoPersonaje1;
    public string videoPersonaje2;
    public string videoCabecera;
    public string videoLoop;
    public GameObject video1;
    public GameObject panelSeleccionar;
    public GameObject panelNombre;
    public string sceneName;

    private enum VideoState
    {
        Cabecera,
        Loop,
        Personaje
    }

    private VideoState currentState;
    private bool loopPreloaded = false;

    void Awake()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.source = VideoSource.Url;

        // Configurar el VideoPlayer de precarga
        videoPlayerPreload.source = VideoSource.Url;
        videoPlayerPreload.playOnAwake = false;
        videoPlayerPreload.renderMode = VideoRenderMode.APIOnly; // No renderiza nada, solo precarga
    }

    void Update()
    {
        if (videoPlayer.isPlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            FinishVideo();
        }
    }

    string GetVideoURL(string relativePath)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return Application.streamingAssetsPath + "/" + relativePath;
#else
        return "file://" + Path.Combine(Application.streamingAssetsPath, relativePath);
#endif
    }

    public void StartVideo()
    {
        panelNombre.SetActive(false);
        video1.SetActive(true);
        currentState = VideoState.Cabecera;
        videoPlayer.url = GetVideoURL("Video/" + videoCabecera);
        StartCoroutine(PlayPrepared());

        // Iniciar precarga del Loop en paralelo
        loopPreloaded = false;
        StartCoroutine(PreloadLoop());
    }

    IEnumerator PreloadLoop()
    {
        videoPlayerPreload.url = GetVideoURL("Video/" + videoLoop);
        videoPlayerPreload.Prepare();
        while (!videoPlayerPreload.isPrepared)
            yield return null;

        loopPreloaded = true;
        Debug.Log("Loop preloaded!");
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        switch (currentState)
        {
            case VideoState.Cabecera:
                panelSeleccionar.SetActive(true);
                currentState = VideoState.Loop;

                if (loopPreloaded)
                {
                    // Usar directamente el VideoPlayer preload ya listo
                    StartCoroutine(SwitchToPreloadedLoop());
                }
                else
                {
                    // Fallback: preparar normalmente si aún no terminó la precarga
                    videoPlayer.url = GetVideoURL("Video/" + videoLoop);
                    videoPlayer.isLooping = true;
                    StartCoroutine(PlayPrepared());
                }
                break;

            case VideoState.Loop:
                break;

            case VideoState.Personaje:
                SceneSelector.Instance.StartNewGame();
                break;
        }
    }

    IEnumerator SwitchToPreloadedLoop()
    {
        videoPlayer.Stop();

        // Copiar la textura preparada del preload al videoPlayer principal
        videoPlayer.url = videoPlayerPreload.url;
        videoPlayer.isLooping = true;

        // Como ya está preparado en otro player, podemos reusar la URL directamente
        // El Prepare() será muy rápido porque el sistema ya tiene el archivo en caché
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayerPreload.Stop();
        videoPlayer.Play();
    }

    void PlayVideo(string videoPath)
    {
        video1.SetActive(true);
        panelSeleccionar.SetActive(false);
        videoPlayer.url = GetVideoURL(videoPath);
        StartCoroutine(PlayPrepared());
    }

    public void PlayPersonaje1()
    {
        currentState = VideoState.Personaje;
        videoPlayer.isLooping = false;
        PlayVideo("Video/" + videoPersonaje1);
    }

    public void PlayPersonaje2()
    {
        currentState = VideoState.Personaje;
        videoPlayer.isLooping = false;
        PlayVideo("Video/" + videoPersonaje2);
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