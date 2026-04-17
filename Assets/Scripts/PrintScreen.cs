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
    private static extern void MostrarDialogoImpresion(string base64);

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
        // ── Navegador Web ──────────────────────────────
        MostrarDialogoImpresion(base64);

#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        // ── Windows y macOS ────────────────────────────
        // Genera un HTML local con la imagen embebida en base64
        // y lo abre en el navegador predeterminado del sistema.
        // El navegador mostrará el diálogo de configuración de impresión
        // donde el usuario elige impresora, tamaño, orientación, etc.
        AbrirDialogoDesktop(base64);
#else
        // ── Fallback ───────────────────────────────────
        string ruta = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "captura_unity.jpg");
        System.IO.File.WriteAllBytes(ruta, bytes);
        Application.OpenURL("file://" + ruta);
        Debug.LogWarning("[Print] Plataforma no reconocida, abriendo archivo.");
#endif
    }

    private void AbrirDialogoDesktop(string base64)
    {
        // HTML con la imagen en base64 + botón de impresión visible
        // window.print() abre el panel de configuración del navegador
        string html = @"<!DOCTYPE html>
<html lang='es'>
<head>
  <meta charset='UTF-8'/>
  <title>Vista previa de impresión</title>
  <style>
    * { margin: 0; padding: 0; box-sizing: border-box; }

    body {
      font-family: Arial, sans-serif;
      background: #f0f0f0;
      display: flex;
      flex-direction: column;
      align-items: center;
      padding: 20px;
      gap: 16px;
    }

    /* Barra de herramientas — oculta al imprimir */
    #toolbar {
      display: flex;
      gap: 12px;
      align-items: center;
      background: #222;
      color: #fff;
      padding: 12px 24px;
      border-radius: 8px;
      width: 100%;
      max-width: 800px;
      justify-content: space-between;
    }

    #toolbar span { font-size: 14px; opacity: 0.8; }

    button {
      background: #4CAF50;
      color: white;
      border: none;
      padding: 10px 28px;
      font-size: 16px;
      border-radius: 6px;
      cursor: pointer;
      transition: background 0.2s;
    }

    button:hover { background: #388E3C; }

    /* Vista previa de la imagen */
    #preview {
      background: white;
      box-shadow: 0 2px 12px rgba(0,0,0,0.2);
      max-width: 800px;
      width: 100%;
      padding: 10px;
    }

    #preview img {
      display: block;
      width: 100%;
      height: auto;
    }

    /* Al imprimir: ocultar toolbar, quitar fondo y márgenes */
    @media print {
      body   { background: white; padding: 0; }
      #toolbar { display: none !important; }
      #preview { box-shadow: none; padding: 0; max-width: 100%; }
      #preview img { width: 100%; }
    }
  </style>
</head>
<body>

  <div id='toolbar'>
    <span>📄 Vista previa — Configura tu impresora antes de imprimir</span>
    <button onclick=""window.print()"">🖨️ Configurar e Imprimir</button>
  </div>

  <div id='preview'>
    <img src='data:image/jpeg;base64," + base64 + @"' alt='Captura'/>
  </div>

</body>
</html>";

        // Guarda el HTML en la carpeta temporal del sistema
        string htmlPath = System.IO.Path.Combine(
            System.IO.Path.GetTempPath(), "unity_print_preview.html");

        System.IO.File.WriteAllText(htmlPath, html, System.Text.Encoding.UTF8);

        // Abre en el navegador predeterminado
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = htmlPath,
            UseShellExecute = true   // delega al SO para abrir con el navegador
        });

        Debug.Log("[Print] Vista previa abierta en: " + htmlPath);
    }
}