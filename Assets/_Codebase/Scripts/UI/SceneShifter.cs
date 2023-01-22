using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneShifter : MonoBehaviour
{

    public void ShiftToPlay()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene("JackTest");
    }

    public void ShiftToWin(){
        SceneManager.LoadScene("TestWin");
    }
    public void ShiftToLose(){
        SceneManager.LoadScene("TestLose");
    }
}
