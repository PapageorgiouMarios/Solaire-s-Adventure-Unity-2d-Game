using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
    public void MainMenu()
    {
        Debug.Log("Scene: MainMenu");
        SceneManager.LoadScene(0);
    }
    
    public void GameScene()
    {
        Debug.Log("Scene: StartGame");
        SceneManager.LoadScene(1);
    }

    public void EndScene()
    {
        Debug.Log("Scene: EndScene");
        SceneManager.LoadScene(2);
    }
    
    public void Quit()
    {
        Debug.Log("Scene: Quit");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
