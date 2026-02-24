using UnityEngine;

public class NarratorAudio : MonoBehaviour
{
    public GameObject targetObject;
    public AudioClip player1, player2;
    public bool enable = false;
    private AudioManager audioManager;
    private bool played;

    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    void Update()
    {
        if (!played && targetObject.activeInHierarchy)
        {
            played = true;

            int character = UserDataLoader.LoadCharacter();
            AudioClip clip = character == 1 ? player1 : player2;
            gameObject.SetActive(enable);
            audioManager.PlayNarrator(clip);
        }
    }
}
