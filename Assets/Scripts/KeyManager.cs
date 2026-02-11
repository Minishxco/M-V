using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject[] fullKeyImage;
    public AudioManager audioManager;
    void Start()
    {
        foreach (var fullKey in fullKeyImage)
        {
            fullKey.SetActive(false);
        }
    }

    public void updateKey()
    {
        audioManager.PlayKeyCollected();
        for (int i = 0; i < fullKeyImage.Length; i++)
        {
            fullKeyImage[0].SetActive(true);
        }
        fadeOut.SetActive(true);
    }
}
