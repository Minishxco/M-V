using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string urlVideo = "Video/FONDO_COCHE_FUEGO.mp4";

    void Start()
    {
        // Ruta al video en StreamingAssets
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, urlVideo);
        videoPlayer.url = path;
        videoPlayer.Play();

        Debug.Log(path);
    }
}