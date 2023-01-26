using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resume(){
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit(){
        SceneManager.LoadScene("TestMenu");
    }
}
