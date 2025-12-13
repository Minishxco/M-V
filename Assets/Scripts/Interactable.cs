using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject objetoAControlar;
    public GameObject selector;

    void Start()
    {
        if (selector != null)
            selector.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Objeto detectado dentro del collider: " + other.name);
        if (objetoAControlar != null && selector != null) 
        { 
            objetoAControlar.SetActive(true); 
        } 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Objeto salió del collider: " + other.name);
        if (objetoAControlar != null && selector != null) 
        { 
            objetoAControlar.SetActive(false); 
        } 
    }

    public void ActiveSelector()
    {
        if (selector != null)
        {
            selector.SetActive(!selector.activeSelf);
        }
    }
}
