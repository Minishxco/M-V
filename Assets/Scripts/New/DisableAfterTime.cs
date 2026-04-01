using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    [SerializeField] float delayAfterDisable = 5f;
    [SerializeField] GameObject[] stuffToDisable;

    void Start()
    {
        Invoke(nameof(DisableObjects), delayAfterDisable);
    }

    void DisableObjects()
    {
        foreach (var obj in stuffToDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }
}