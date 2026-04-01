using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using System.IO;

public class PlayVideoOnStart : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public string videoPersonaje1;
    public string videoPersonaje2;

    private string selectedVideoPath;

    public GameObject fadeObject;
    public float fadeDuration = 1f;

    public GameObject canvasVideo;

    void Start()
    {
        StartCoroutine(InitSequence());
    }

    IEnumerator InitSequence()
    {
        if (fadeObject != null)
            fadeObject.SetActive(true);

        yield return new WaitForSeconds(fadeDuration);

        int character = UserDataLoader.LoadCharacter();

        selectedVideoPath = character == 1
            ? videoPersonaje1
            : videoPersonaje2;

        if (canvasVideo != null)
            canvasVideo.SetActive(true);

        videoPlayer.source = VideoSource.Url;
        videoPlayer.isLooping = false;
        videoPlayer.playOnAwake = false;

        string fullPath = GetVideoURL("Video/" + selectedVideoPath);
        videoPlayer.url = fullPath;

        videoPlayer.loopPointReached += OnVideoFinished;

        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayer.Play();
    }

    string GetVideoURL(string relativePath)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return Application.streamingAssetsPath + "/" + relativePath;
#else
        return "file://" + Path.Combine(Application.streamingAssetsPath, relativePath);
#endif
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        StartCoroutine(FinishSequence());
    }

    IEnumerator FinishSequence()
    {
        if (fadeObject != null)
            fadeObject.SetActive(true);

        yield return new WaitForSeconds(fadeDuration);

        videoPlayer.Stop();

        if (videoPlayer.targetTexture != null)
        {
            RenderTexture.active = videoPlayer.targetTexture;
            GL.Clear(true, true, Color.clear);
            RenderTexture.active = null;
        }

        if (canvasVideo != null)
            canvasVideo.SetActive(false);
    }
}