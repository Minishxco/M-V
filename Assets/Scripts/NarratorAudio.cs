using UnityEngine;

public class narratorAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip player1, player2;
    private int character;
    private void OnEnable()
    {
        character = UserDataLoader.LoadCharacter();
        if (character == 1)
        {
            audioSource.PlayOneShot(player1);
        }
        else if (character == 2)
        {
            audioSource.PlayOneShot(player2);
        }
    }
}
