using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject[] fullKeyImage;
    public AudioManager audioManager;
    public int key;

    public void updateKey()
    {
        audioManager.PlayKeyCollected();
        for (int i = 0; i < fullKeyImage.Length; i++)
        {
            fullKeyImage[key].SetActive(true);
        }
        if(fadeOut != null)
        {
            fadeOut.SetActive(true);
        }
    }
}
