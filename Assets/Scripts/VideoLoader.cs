using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string urlVideo = "Video/FONDO_COCHE_FUEGO.mov";

    void Start()
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, urlVideo);
        videoPlayer.url = path;

        videoPlayer.isLooping = false;
        videoPlayer.loopPointReached += OnVideoEnd;

        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        vp.frame = 0;
        vp.Play();
    }
}