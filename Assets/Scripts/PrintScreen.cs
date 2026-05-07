using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine.UI;

public class PrintScreen : MonoBehaviour
{
    [Header("Datos")]
    public TextMeshProUGUI textName;
    public Button botonImprimir;

    [Header("Captura por cámara")]
    [Tooltip("Cámara dedicada que renderiza SOLO el diploma. Debe estar desactivada por defecto.")]
    public Camera camaraDiploma;

    [Tooltip("Resolución de salida. A4 horizontal a ~200dpi = 2339x1654. A 300dpi = 3508x2480.")]
    public int anchoCaptura = 2339;
    public int altoCaptura = 1654;

    [Tooltip("Calidad JPG (1-100). 90 está bien para impresión.")]
    [Range(1, 100)]
    public int calidadJPG = 92;

    [DllImport("__Internal")]
    private static extern void MostrarDialogoImpresion(string base64);

    private void Awake()
    {
        if (textName != null)
            textName.text = UserDataLoader.LoadName();

        // Asegurar que la cámara empieza apagada
        if (camaraDiploma != null)
            camaraDiploma.gameObject.SetActive(false);
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

        yield return StartCoroutine(CapturarConCamara());
        yield return new WaitForSeconds(1);

        if (botonImprimir != null)
            botonImprimir.gameObject.SetActive(true);
    }

    private IEnumerator CapturarConCamara()
    {
        if (camaraDiploma == null)
        {
            Debug.LogError("[Print] No hay cámara de diploma asignada.");
            yield break;
        }

        // 1) Crear RenderTexture temporal con la resolución que queremos imprimir
        RenderTexture rt = RenderTexture.GetTemporary(
            anchoCaptura, altoCaptura, 24, RenderTextureFormat.ARGB32);
        rt.antiAliasing = 1; // si quieres MSAA pon 2, 4 u 8 (cuidado con WebGL)

        // 2) Asignar el RT a la cámara y activarla
        RenderTexture rtAnterior = camaraDiploma.targetTexture;
        camaraDiploma.targetTexture = rt;
        camaraDiploma.gameObject.SetActive(true);

        // 3) Esperar al final del frame para que la cámara renderice
        yield return new WaitForEndOfFrame();

        // 4) Forzar render por si la cámara no rendea automáticamente
        camaraDiploma.Render();

        // 5) Leer pixeles desde el RT
        RenderTexture rtActivoAnterior = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D tex = new Texture2D(anchoCaptura, altoCaptura, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, anchoCaptura, altoCaptura), 0, 0);
        tex.Apply();

        RenderTexture.active = rtActivoAnterior;

        // 6) Apagar cámara y liberar RT
        camaraDiploma.targetTexture = rtAnterior;
        camaraDiploma.gameObject.SetActive(false);
        RenderTexture.ReleaseTemporary(rt);

        // 7) Codificar a JPG + base64
        byte[] bytes = tex.EncodeToJPG(calidadJPG);
        string base64 = System.Convert.ToBase64String(bytes);
        Destroy(tex);

#if UNITY_WEBGL && !UNITY_EDITOR
        MostrarDialogoImpresion(base64);
#else
        Debug.LogWarning("[Print] Plataforma no reconocida.");
#endif
    }

}
