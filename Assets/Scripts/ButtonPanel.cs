using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ButtonPanel : MonoBehaviour
{
    public GameObject[] panelABCD;

    public void Dialogo(int num)
    {
        foreach (GameObject panel in panelABCD) {
            panel.SetActive(false);
        }
        panelABCD[num].SetActive(true);
    }
}
