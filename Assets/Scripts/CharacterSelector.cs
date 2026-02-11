using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [Header("Images")]
    public GameObject character1Image;
    public GameObject character2Image;

    private int selectedCharacter = 1; // 1 o 2
    private int maxCharacters = 2;

    public VideoSelector videoSelector;
    public UserDataSaver dataSaver;
    public GameObject panelSeccionar;

    private void Start()
    {
        SelectCharacter2();
    }

    private void Update()
    {
        if (!panelSeccionar.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            NextCharacter();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            PreviousCharacter();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmSelectedCharacter();
        }
    }

    public void NextCharacter()
    {
        selectedCharacter++;

        if (selectedCharacter > maxCharacters)
            selectedCharacter = 1;

        UpdateSelection();
    }

    public void PreviousCharacter()
    {
        selectedCharacter--;

        if (selectedCharacter < 1)
            selectedCharacter = maxCharacters;

        UpdateSelection();
    }

    private void UpdateSelection()
    {
        if (selectedCharacter == 1)
            SelectCharacter1();
        else
            SelectCharacter2();
    }

    public void SelectCharacter1()
    {
        selectedCharacter = 1;
        character1Image.SetActive(true);
        character2Image.SetActive(false);
    }

    public void SelectCharacter2()
    {
        selectedCharacter = 2;
        character2Image.SetActive(true);
        character1Image.SetActive(false);
    }

    private void ConfirmSelectedCharacter()
    {
        if (selectedCharacter == 1)
        {
            ConfirmCharacter1();
        }
        else
        {
            ConfirmCharacter2();
        }
    }

    public void ConfirmCharacter1()
    {
        Debug.Log("Personaje 1 confirmado");
        videoSelector.PlayPersonaje2();
        dataSaver.SelectCharacter2();
    }

    public void ConfirmCharacter2()
    {
        Debug.Log("Personaje 2 confirmado");
        videoSelector.PlayPersonaje1();
        dataSaver.SelectCharacter1();
    }
}
