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
            PlayerPrefs.SetInt("Score", 0);
        }
        SceneManager.LoadScene(levelName);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        
#else
Application.Quit();
#endif
    }
}
