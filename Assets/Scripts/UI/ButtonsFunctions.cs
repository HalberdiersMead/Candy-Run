using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsFunctions : MonoBehaviour
{
    public void Play(string levelName)
    {
        if(levelName=="MainMenu" || levelName=="LevelSelect")
        {
            if (PlayerPrefs.HasKey("Score"))
            {
                if(PlayerPrefs.GetInt("HighScore")< PlayerPrefs.GetInt("Score"))
                {
                    PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
                }
                    
            }
            PlayerPrefs.SetInt("Score", 0);
        }
        SceneManager.LoadScene(levelName);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        PlayerPrefs.DeleteAll();
#else
Application.Quit();
#endif
    }
}
