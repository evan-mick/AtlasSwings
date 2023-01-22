using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class WinCondition : MonoBehaviour{
    bool m_IsPlayerAtExit;
    public static int strokes;
    public static int seconds;

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            m_IsPlayerAtExit = true;
        }
    }

    void Update ()
    {
        if(m_IsPlayerAtExit)
        {
            EndLevel ();
        }
    }

    void EndLevel ()
    {
        strokes = GameObject.Find("ProjectileCreator").GetComponent<BallLauncher>().numberStrokes;
        seconds = GameObject.Find("Timer").GetComponent<Timer>().numSeconds;
        PlayerPrefs.SetInt("strokes", strokes);
        PlayerPrefs.SetInt("time", seconds);
        SceneManager.LoadScene("TestWin");
    }
}
