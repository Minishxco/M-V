using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

public class VideoSelector : MonoBehaviour
{
    public VideoPlayer videoPlayer;

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

    void Awake()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.source = VideoSource.Url;
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
    }

    public void PlayPersonaje1()
    {
        currentState = VideoState.Personaje;
        PlayVideo("Video/" + videoPersonaje1);
    }

    public void PlayPersonaje2()
    {
        currentState = VideoState.Personaje;
        PlayVideo("Video/" + videoPersonaje2);
    }

    void PlayVideo(string videoPath)
    {
        video1.SetActive(true);
        panelSeleccionar.SetActive(false);

        videoPlayer.url = GetVideoURL(videoPath);
        StartCoroutine(PlayPrepared());
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        switch (currentState)
        {
            case VideoState.Cabecera:
                panelSeleccionar.SetActive(true);
                currentState = VideoState.Loop;
                videoPlayer.url = GetVideoURL("Video/" + videoLoop);
                StartCoroutine(PlayPrepared());
                break;

            case VideoState.Personaje:
                SceneSelector.Instance.StartNewGame();
                //SceneManager.LoadScene(sceneName);
                break;
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