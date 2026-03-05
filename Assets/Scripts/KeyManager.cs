using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject[] fullKeyImage;
    public AudioManager audioManager;

    public int initialKeys = 0;
    private int key;

    void Start()
    {
        key = initialKeys;

        for (int i = 0; i < key && i < fullKeyImage.Length; i++)
        {
            fullKeyImage[i].SetActive(true);
        }
    }

    public void updateKey()
    {
        if (key >= fullKeyImage.Length) return;

        audioManager.PlayKeyCollected();

        fullKeyImage[key].SetActive(true);
        key++;

        if (fadeOut != null)
        {
            fadeOut.SetActive(true);
        }
    }
}