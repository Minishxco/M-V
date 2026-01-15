using UnityEngine;

public static class UserDataLoader
{
    const string NAME_KEY = "UserName";
    const string CHARACTER_KEY = "UserCharacter";
    const string DATA_KEY = "UserKey";

    public static string LoadName()
    {
        return PlayerPrefs.GetString(NAME_KEY, "");
    }

    public static int LoadCharacter()
    {
        return PlayerPrefs.GetInt(CHARACTER_KEY, 0);
    }

    public static int LoadGame()
    {
        return PlayerPrefs.GetInt(DATA_KEY, 0);
    }
}
