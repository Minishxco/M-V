using UnityEngine;
using System.IO;
using System.Diagnostics;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class PrintScreen : MonoBehaviour
{
    private string rutaTemporal;
    public TextMeshProUGUI textName;
    public Button botonImprimir;

    private void Awake()
    {
        if (textName != null)
        {
            textName.text = UserDataLoader.LoadName();
        }
    }

    public void CapturarEImprimir()
    {
        StartCoroutine(ProcesoCaptura());
    }

    private IEnumerator ProcesoCaptura()
    {
        if (botonImprimir != null)
            botonImprimir.gameObject.SetActive(false);

        yield return null;

        yield return StartCoroutine(Capturar());

        yield return new WaitForSeconds(1f);

        if (botonImprimir != null)
            botonImprimir.gameObject.SetActive(true);
    }

    private IEnumerator Capturar()
    {
        yield return new WaitForEndOfFrame();

        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        rutaTemporal = Path.Combine(Path.GetTempPath(), "captura_unity.jpg");
        File.WriteAllBytes(rutaTemporal, tex.EncodeToJPG(100));
        Destroy(tex);

        ProcessStartInfo psi = new ProcessStartInfo();
        psi.FileName = rutaTemporal;
        psi.Verb = "print";
        psi.CreateNoWindow = true;
        psi.WindowStyle = ProcessWindowStyle.Hidden;

        Process.Start(psi);
    }
}