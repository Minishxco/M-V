using UnityEngine;

public class MissionsDone : MonoBehaviour
{
    public static MissionsDone Instance;

    [SerializeField] GameObject[] keys;


    int missionsDone;

    private void Awake()
    {
        Instance = this;

        missionsDone = PlayerPrefs.GetInt("_MissionsDone", 0);

        Debug.Log("misiones:" + missionsDone);
    }

    private void Start()
    {
        for (int i = 0; i < missionsDone && i < keys.Length; i++)
        {
            keys[i].SetActive(false);
        }
    }

    public int GetMissionsDone()
    {
        return missionsDone;
    }

}
