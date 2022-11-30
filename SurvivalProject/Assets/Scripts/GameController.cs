using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject birdsSound;

    public GameObject deathSound;
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
       
        // Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        PlayerStatus.OnPlayerDeath -= EnableGameOVerMenu;
        
    }

    public void EnableGameOVerMenu()
    {
        gameOverMenu.SetActive(true);
        birdsSound.SetActive(false);
        deathSound.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
