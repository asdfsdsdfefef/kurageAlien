using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public static void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}