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
    public GameObject[] enableAfterVideo;

    public bool goToSceneOnFinish = true;

    public GameObject canvasVideo;

    void Awake()
    {
        videoPlayer.source = VideoSource.Url;
        videoPlayer.isLooping = false;
        videoPlayer.playOnAwake = false;

        videoPlayer.loopPointReached += OnVideoFinished;

        // LIMPIAR RENDER TEXTURE
        if (videoPlayer.targetTexture != null)
        {
            RenderTexture.active = videoPlayer.targetTexture;
            GL.Clear(true, true, Color.clear);
            RenderTexture.active = null;
        }

        int character = UserDataLoader.LoadCharacter();

        selectedVideoPath = character == 1
            ? videoPersonaje1
            : videoPersonaje2;
    }

    public void PlayVideoFromButton()
    {
        if (disableObjects != null)
        {
            foreach (var obj in disableObjects)
            {
                obj.SetActive(false);
            }
        }

        if(canvasVideo != null)
        {
            canvasVideo.SetActive(true);
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
            SceneSelector.Instance.CompleteCurrentMission();
            //SceneManager.LoadScene(sceneName);
        }
        else
        {
            videoPlayer.Stop();
            videoPlayer.gameObject.SetActive(false);

            if (disableObjects != null)
            {
                foreach (var obj in disableObjects)
                {
                    obj.SetActive(true);
                }
            }

            if(disableAfterVideo != null) {
                foreach (var obj in disableAfterVideo)
                {
                    obj.SetActive(false);
                }
            }

            if (enableAfterVideo != null)
            {
                foreach (var obj in enableAfterVideo)
                {
                    obj.SetActive(true);
                }
            }

        }
    }
}