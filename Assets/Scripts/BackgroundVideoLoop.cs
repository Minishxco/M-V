using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using System.IO;

[RequireComponent(typeof(VideoPlayer))]
public class BackgroundVideoLoop : MonoBehaviour
{
    public string videoFileName;
    public Camera targetCamera;
    public float planeDistance = 1f;

    private VideoPlayer videoPlayer;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.source = VideoSource.Url;
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = true;
        videoPlayer.waitForFirstFrame = true;
        videoPlayer.skipOnDrop = true;

        videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
        videoPlayer.targetCamera = targetCamera;
        videoPlayer.targetCameraAlpha = 1f;

        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
    }

    void Start()
    {
        videoPlayer.url = GetVideoPath(videoFileName);
        StartCoroutine(PrepareAndPlay());
    }

    IEnumerator PrepareAndPlay()
    {
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayer.Play();
    }

    string GetVideoPath(string fileName)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return Application.streamingAssetsPath + "/" + fileName;
#else
        return "file://" + Path.Combine(Application.streamingAssetsPath, fileName);
#endif
    }
}