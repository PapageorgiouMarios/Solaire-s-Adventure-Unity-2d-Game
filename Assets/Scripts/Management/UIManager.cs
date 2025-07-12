using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; } // This instance will be used in all scenes
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private AudioClip gameOverSound;

    private void Awake()
    {
        instance = this;

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (pauseScreen.activeInHierarchy) 
            {
                PauseGame(false);
            }
            else 
            {
                PauseGame(true);
            }
        }
    }

    #region Game Over Screen
    public void GameOver() 
    {
        SoundManager.instance.StopMusic();
        SoundManager.instance.PlaySound(gameOverSound);
        gameOverScreen.SetActive(true);
    }

    public void Restart() 
    {
        instance = null;
        Destroy(gameObject);

        GameManager.instance.DestroyAfterRestart();
        PlayerLife.instance.DestroyAfterRestart();

        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        instance = null;
        Destroy(gameObject);

        GameManager.instance.DestroyAfterRestart();
        PlayerLife.instance.DestroyAfterRestart();

        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        instance = null;
        Destroy(gameObject);

        GameManager.instance.DestroyAfterRestart();
        PlayerLife.instance.DestroyAfterRestart();

        Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif

    }
    #endregion

    #region Pause Game
    public void PauseGame(bool status) 
    {
        pauseScreen.SetActive(status);
        if(status) 
        {
            Time.timeScale = 0;
        }
        else 
        {
            Time.timeScale = 1;
        }
    }

    public void SoundVolume() 
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }

    public void DestroyInComplete() 
    {
        instance = null;
        Destroy(gameObject);
    }

    #endregion

}
