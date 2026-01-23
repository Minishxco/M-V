using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public GameObject[] fullKeyImage;
    void Start()
    {
        foreach (var fullKey in fullKeyImage)
        {
            fullKey.SetActive(false);
        }
    }

    public void updateKey()
    {
        for (int i = 0; i < fullKeyImage.Length; i++)
        {
            fullKeyImage[0].SetActive(true);
        }
    }
}
