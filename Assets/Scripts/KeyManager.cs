using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject[] fullKeyImage;
    public AudioManager audioManager;

    public bool enableVideo = false;
    public PlayVideo playVideo;
    public float correctAnimDuration = 2f;
    public float delayBeforeFade = 1f;
    public float fadeDuration = 1f;



    public void updateKey()
    {
        int missionsDone = PlayerPrefs.GetInt("_MissionsDone", 0);
        fullKeyImage[missionsDone].SetActive(true);
        missionsDone++;
        PlayerPrefs.SetInt("_MissionsDone", missionsDone);


        if (missionsDone >= fullKeyImage.Length) return;
        audioManager.PlayKeyCollected();
        
        if (fadeOut != null)
        {
            fadeOut.SetActive(true);

            if(enableVideo)
            {
                StartCoroutine(SecuenciaCorrecta());
            }
        }
    }

    IEnumerator SecuenciaCorrecta()
    {
        yield return new WaitForSeconds(correctAnimDuration);

        yield return new WaitForSeconds(delayBeforeFade);

        fadeOut.SetActive(true);

        yield return new WaitForSeconds(fadeDuration);

        playVideo.PlayVideoFromButton();
    }
}