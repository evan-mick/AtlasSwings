using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroCinematic : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer _videoPlayer;
    [SerializeField]
    private string _nextScene;
    
    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0.0f;
        _videoPlayer.loopPointReached += GotoNextScene;
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            GotoNextScene(_videoPlayer);
        }*/
    }

    private void GotoNextScene(VideoPlayer source)
    {
        SceneManager.LoadScene(_nextScene);
    }
    /*private void UnsetScale(VideoPlayer source)
    {
        Time.timeScale = 1.0f; 
    }*/

}
