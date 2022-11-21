using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public GameObject gameOverMenu;
    public void ChangeScene(int sceneNumber) {
        SceneManager.LoadScene(sceneNumber);
    }

    public void QuitGame() {
        Application.Quit();
    }

    private void OnEnable() 
    {
        PlayerStatus.OnPlayerDeath += EnableGameOVerMenu;
        Debug.Log("OnEnable");
       // Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        PlayerStatus.OnPlayerDeath -= EnableGameOVerMenu;
        Debug.Log("OnDisable");
    }

    public void EnableGameOVerMenu()
    {
        gameOverMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
