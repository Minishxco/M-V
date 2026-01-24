using UnityEngine;

public class NarratorAudio : MonoBehaviour
{
    public GameObject targetObject;
    public AudioClip player1, player2;

    private bool played;

    void Update()
    {
        if (!played && targetObject.activeInHierarchy)
        {
            played = true;

            int character = UserDataLoader.LoadCharacter();
            AudioClip clip = character == 1 ? player1 : player2;

            AudioManager.Instance.PlayNarrator(clip);
        }
    }
}
