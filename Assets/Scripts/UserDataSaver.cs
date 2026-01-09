using UnityEngine;
using TMPro;

public class UserDataSaver : MonoBehaviour
{
    public TMP_InputField nameInput;

    const string NAME_KEY = "UserName";
    const string CHARACTER_KEY = "UserCharacter";
    public VideoSelector videoSelector;

    public void SaveName()
    {
        if (string.IsNullOrEmpty(nameInput.text))
        {
            Debug.Log("El nombre está vacío");
            return;
        }

        PlayerPrefs.SetString(NAME_KEY, nameInput.text);
        PlayerPrefs.Save();

        Debug.Log("Nombre guardado: " + nameInput.text);
        videoSelector.StartVideo();
    }

    public void SelectCharacter1()
    {
        PlayerPrefs.SetInt(CHARACTER_KEY, 1);
        PlayerPrefs.Save();
        Debug.Log("Personaje 1 seleccionado");
    }

    public void SelectCharacter2()
    {
        PlayerPrefs.SetInt(CHARACTER_KEY, 2);
        PlayerPrefs.Save();
        Debug.Log("Personaje 2 seleccionado");
    }
}
