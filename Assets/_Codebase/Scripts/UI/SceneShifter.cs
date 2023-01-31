using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneShifter : MonoBehaviour
{

    public void ShiftToPlay()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        Time.timeScale = 1;
        SceneManager.LoadScene("ImplementedModelScene");
    }
    
    public void Quit(){
        Application.Quit();
    }
}
