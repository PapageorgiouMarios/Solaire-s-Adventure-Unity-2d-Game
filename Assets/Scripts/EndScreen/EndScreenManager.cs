using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    
    [SerializeField] private float duration = 5f;
    private float timer = 0f;

    private void Start()
    {
        timer = 0f;
    }


    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log("timer: " + timer);
        if (timer >= duration)
        {
            SceneManager.LoadScene(0);
            timer = 0f;
        }
    }
}
