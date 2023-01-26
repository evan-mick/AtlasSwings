using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneShifter : MonoBehaviour
{
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _winScreen;

    public void ShiftToPlay()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene("ImplementedModelScene");
    }

    public void ShiftToWin(){
        _winScreen.SetActive(true);
        Time.timeScale = 0;
        //SceneManager.LoadScene("TestWin");
    }
    public void ShiftToLose(){
        _loseScreen.SetActive(true);
        Time.timeScale = 0;
        //SceneManager.LoadScene("TestLose");
    }
}
