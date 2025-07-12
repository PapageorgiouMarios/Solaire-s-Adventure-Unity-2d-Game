using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void GameScene()
    {
        Debug.Log("Scene: StartGame");
        SceneManager.LoadSceneAsync(1);
    }
    
    public void Quit()
    {
        Debug.Log("Scene: Quit");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
