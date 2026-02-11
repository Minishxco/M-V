using UnityEngine;
using UnityEngine.Events;

public class KeyCall: MonoBehaviour
{
    public UnityEvent onEnterPressed;
    public KeyCode key = KeyCode.Return;

    void Update()
    {
        if (Input.GetKeyDown(key))
            onEnterPressed?.Invoke();
    }
}
