using UnityEngine;
using UnityEngine.Video;
using System.IO;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string videoPersonaje1;
    public string videoPersonaje2;

    private string selectedVideoPath;

    public GameObject[] disableObjects;

    public GameObject[] disableAfterVideo;

    public string sceneName;
    public bool goToSceneOnFinish = true;

    void Awake()
    {
        videoPlayer.source = VideoSource.Url;
        videoPlayer.isLooping = false;
        videoPlayer.playOnAwake = false;

        videoPlayer.loopPointReached += OnVideoFinished;

        int character = UserDataLoader.LoadCharacter();

        selectedVideoPath = character == 1
            ? videoPersonaje1
            : videoPersonaje2;
    }

    public void PlayVideoFromButton()
    {
        foreach (var obj in disableObjects)
        {
            obj.SetActive(false);
        }

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
        if (goToSceneOnFinish)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            videoPlayer.Stop();
            videoPlayer.gameObject.SetActive(false);

            foreach (var obj in disableObjects)
            {
                obj.SetActive(true);
            }

            foreach (var obj in disableAfterVideo)
            {
                obj.SetActive(false);
            }
        }
    }
}