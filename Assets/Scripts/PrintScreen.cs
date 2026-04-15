using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine.UI;

public class PrintScreen : MonoBehaviour
{
    public TextMeshProUGUI textName;
    public Button botonImprimir;

    [DllImport("__Internal")]
    private static extern void ImprimirImagen(string base64);

    private void Awake()
    {
        if (textName != null)
            textName.text = UserDataLoader.LoadName();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (botonImprimir != null && botonImprimir.gameObject.activeSelf)
                botonImprimir.onClick.Invoke();
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

        yield return StartCoroutine(Capturar());

        if (botonImprimir != null)
            botonImprimir.gameObject.SetActive(true);
    }

    private IEnumerator Capturar()
    {
        yield return new WaitForEndOfFrame();

        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        byte[] bytes = tex.EncodeToJPG(90);
        string base64 = System.Convert.ToBase64String(bytes);
        Destroy(tex);

#if UNITY_WEBGL && !UNITY_EDITOR
        ImprimirImagen(base64);
#else
        string ruta = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "captura_unity.jpg");
        System.IO.File.WriteAllBytes(ruta, bytes);
        Application.OpenURL("file://" + ruta);
        UnityEngine.Debug.Log("Captura guardada en: " + ruta);
#endif
    }
}