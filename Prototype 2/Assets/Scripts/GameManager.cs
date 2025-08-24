using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static GameManager Instance;
    // Start is called before the first frame update

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

    }
    void Start()
    {
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Debug.Log("restart");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
