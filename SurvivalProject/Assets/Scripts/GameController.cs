using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject gameOverMenu;
    public GameObject birdsSound;
    public GameObject SettingsMenu;

    public GameObject waterSound;
    public GameObject riverSound;

    public GameObject deathSound;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        SettingsMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        waterSound.SetActive(true);
        birdsSound.SetActive(true);
        riverSound.SetActive(true);

    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        waterSound.SetActive(false);
        birdsSound.SetActive(false);
        riverSound.SetActive(false);

    }

    public void GoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }



    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        PlayerStatus.OnPlayerDeath += EnableGameOVerMenu;
    }

    private void OnDisable()
    {
        PlayerStatus.OnPlayerDeath -= EnableGameOVerMenu;

    }

    public void EnableGameOVerMenu()
    {
        gameOverMenu.SetActive(true);
        birdsSound.SetActive(false);
        waterSound.SetActive(false);
        riverSound.SetActive(false);
        deathSound.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
