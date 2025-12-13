using UnityEngine;
using System.IO;
using System.Diagnostics;
using System.Collections;

public class PrintScreen : MonoBehaviour
{
    private string rutaTemporal;

    public void CapturarEImprimir()
    {
        rutaTemporal = Path.Combine(Path.GetTempPath(), "captura_unity.jpg");
        StartCoroutine(Capturar());
    }

    private IEnumerator Capturar()
    {
        yield return new WaitForEndOfFrame();

        // Captura
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        // Guardar automática
        File.WriteAllBytes(rutaTemporal, tex.EncodeToJPG(100));
        Destroy(tex);

        // Mandar a imprimir por Windows
        ProcessStartInfo psi = new ProcessStartInfo();
        psi.FileName = rutaTemporal;
        psi.Verb = "print";     // <- imprime con impresora predeterminada
        psi.CreateNoWindow = true;
        psi.WindowStyle = ProcessWindowStyle.Hidden;

        Process.Start(psi);
    }
}
