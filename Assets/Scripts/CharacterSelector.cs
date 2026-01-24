using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [Header("Images")]
    public GameObject character1Image;
    public GameObject character2Image;

    private int selectedCharacter = 0;

    public VideoSelector videoSelector;
    public UserDataSaver dataSaver;

    private void Start()
    {
        SelectCharacter2();
    }

    public void SelectCharacter1()
    {
        if (selectedCharacter == 1)
        {
            ConfirmCharacter1();
            return;
        }

        selectedCharacter = 1;
        character1Image.SetActive(true);
        character2Image.SetActive(false);
    }

    public void SelectCharacter2()
    {
        if (selectedCharacter == 2)
        {
            ConfirmCharacter2();
            return;
        }

        selectedCharacter = 2;
        character2Image.SetActive(true);
        character1Image.SetActive(false);
    }

    // SEGUNDO CLIC
    private void ConfirmCharacter1()
    {
        Debug.Log("Personaje 1 confirmado");
        videoSelector.PlayPersonaje2();
        dataSaver.SelectCharacter2();
    }

    private void ConfirmCharacter2()
    {
        Debug.Log("Personaje 2 confirmado");
        videoSelector.PlayPersonaje1();
        dataSaver.SelectCharacter1();
    }
}
