using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

public class PlayerIntroVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string videoPersonaje1;
    public string videoPersonaje2;
    public GameObject PanelVideo;
    private string selectedVideoPath;

    void Awake()
    {
        videoPlayer.source = VideoSource.Url;
        videoPlayer.loopPointReached += OnVideoFinished;

        int character = UserDataLoader.LoadCharacter();

        selectedVideoPath = character == 1
            ? videoPersonaje1
            : videoPersonaje2;
    }

    public void PlayVideoFromButton()
    {
        string fullPath = GetVideoURL("Video/" + selectedVideoPath);
        videoPlayer.url = fullPath;

        StartCoroutine(PlayPrepared());
    }

    string GetVideoURL(string relativePath)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return Application.streamingAssetsPath + "/" + relativePath;
#else
        return "file://" + Path.Combine(Application.streamingAssetsPath, relativePath);
#endif
    }

    IEnumerator PlayPrepared()
    {
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        PanelVideo.SetActive(false);
    }
}