using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string urlVideo = "Video/FONDO_COCHE_FUEGO.mov";
    [SerializeField] GameObject[] _goToDisappear;

    void Start()
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, urlVideo);
        videoPlayer.url = path;
        videoPlayer.isLooping = false;
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.started += OnVideoStarted; 
        videoPlayer.Play();
    }

    void OnVideoStarted(VideoPlayer vp)
    {
        for (int i = 0; i < _goToDisappear.Length; i++)
        {
            _goToDisappear[i].SetActive(false);
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        vp.frame = 0;
        vp.Play();
    }
}