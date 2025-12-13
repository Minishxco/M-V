using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Objeto que se activará/desactivará")]
    public GameObject objetoAControlar;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Objeto detectado dentro del collider: " + other.name);

        // Activar el objeto
        if (objetoAControlar != null)
        {
            objetoAControlar.SetActive(true);
        }
    }



    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Objeto salió del collider: " + other.name);

        // Desactivar el objeto
        if (objetoAControlar != null)
        {
            objetoAControlar.SetActive(false);
        }
    }
}
