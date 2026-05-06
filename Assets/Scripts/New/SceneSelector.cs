using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneSelector : MonoBehaviour
{
    public static SceneSelector Instance;


    const int MAIN_MENU = 0;
    const int MISSION_1 = 1;          // Solo puede ser la primera misión
    const int MISSION_START = 2;      // Misiones normales: 2-8
    const int MISSION_END = 8;
    const int FINAL_SCENE = 9;

    const int TOTAL_MISSIONS = 4;
    const float MISSION1_CHANCE = 0.25f;

    private void Start()
    {
        Instance = this;
    }

    // -------------------------------------------------------------
    // MENÚ: llama a este método desde el botón "Jugar"
    // -------------------------------------------------------------
    public void StartNewGame()
    {
        ResetSession();
        GenerateMissionSequence();
        LoadCurrentMission();
    }

    // -------------------------------------------------------------
    // PRUEBAS: llama a este método cuando se complete la misión actual
    // -------------------------------------------------------------
    public void CompleteCurrentMission()
    {
        int currentIndex = PlayerPrefs.GetInt("CurrentMissionIndex", 0);
        int nextIndex = currentIndex + 1;
        Debug.Log($"Se termino la mision numero {currentIndex}, pasara a la siguiente");
        if (nextIndex >= TOTAL_MISSIONS)
        {
            Debug.Log($"Mision siguiente numero {nextIndex}, diploma");

            ResetSession();
            SceneManager.LoadScene(FINAL_SCENE);
        }
        else
        {
            PlayerPrefs.SetInt("CurrentMissionIndex", nextIndex);
            UpdateNextIndex(nextIndex);
            PlayerPrefs.Save();
            Debug.Log($"Mision siguiente numero {nextIndex}");

            SceneManager.LoadScene(PlayerPrefs.GetInt("NextSceneIndex"));
        }

    }

    // -------------------------------------------------------------
    // Generación de la secuencia
    // -------------------------------------------------------------
    void GenerateMissionSequence()
    {
        int[] missions = new int[TOTAL_MISSIONS];

        // Pool de misiones normales disponibles (2-8)
        List<int> pool = new();
        for (int s = MISSION_START; s <= MISSION_END; s++) pool.Add(s);

        // --- Misión 1: 25% de probabilidad de ser la PRUEBA 1 ---
        if (Random.value < MISSION1_CHANCE)
        {
            missions[0] = MISSION_1;
        }
        else
        {
            missions[0] = TakeRandom(pool);
        }

        // --- Misiones 2, 3 y 4: cualquier misión normal del pool, sin repetir ---
        // (La PRUEBA 1 nunca entra al pool, así que no puede aparecer aquí)
        for (int i = 1; i < TOTAL_MISSIONS; i++)
        {
            missions[i] = TakeRandom(pool);
        }

        // Guardar secuencia en PlayerPrefs
        for (int i = 0; i < TOTAL_MISSIONS; i++)
        {
            PlayerPrefs.SetInt("Mission_" + i, missions[i]);
        }

        // Empezar en la misión 0
        PlayerPrefs.SetInt("CurrentMissionIndex", 0);
        UpdateNextIndex(0);
        PlayerPrefs.Save();

        Debug.Log($"Secuencia generada: {missions[0]}, {missions[1]}, {missions[2]}, {missions[3]}");
    }

    // Toma un elemento aleatorio del pool y lo elimina (evita repeticiones)
    int TakeRandom(List<int> pool)
    {
        int idx = Random.Range(0, pool.Count);
        int value = pool[idx];
        pool.RemoveAt(idx);
        return value;
    }

    void UpdateNextIndex(int currentMissionIndex)
    {

        if (currentMissionIndex < TOTAL_MISSIONS)
            PlayerPrefs.SetInt("NextSceneIndex", PlayerPrefs.GetInt("Mission_" + currentMissionIndex));
        else
            PlayerPrefs.SetInt("NextSceneIndex", FINAL_SCENE);
    }

    void LoadCurrentMission()
    {
        int currentIndex = PlayerPrefs.GetInt("CurrentMissionIndex", 0);
        int sceneToLoad = PlayerPrefs.GetInt("Mission_" + currentIndex, MISSION_START);
        SceneManager.LoadScene(sceneToLoad);
    }

    // -------------------------------------------------------------
    // Resetea toda la sesión (llamar al volver al menú o salir)
    // -------------------------------------------------------------
    public static void ResetSession()
    {
        PlayerPrefs.DeleteKey("_MissionsDone");
        PlayerPrefs.DeleteKey("CurrentMissionIndex");
        PlayerPrefs.DeleteKey("NextSceneIndex");
        for (int i = 0; i < TOTAL_MISSIONS; i++)
            PlayerPrefs.DeleteKey("Mission_" + i);
        PlayerPrefs.Save();
    }
}