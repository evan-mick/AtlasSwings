using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
  [SerializeField] private Text _timerTextTMP;
  [SerializeField] private Text _strokeTextTMP;
  public static Timer Instance;
  public GameObject projectileLauncher;

  private float _maxTimeValue = 120; // Do not change this! It is tied to the music
  private float _timeValue;
  public int numSeconds { get; private set; }

  // ================== Methods
  
  void Awake() 
  {
    Instance = this;
    ResetForMain();
  }

  public void ResetForMain()
  {
    //_timerTextTMP.color = new Color(0, 0, 0, 0);
    _timeValue = _maxTimeValue;
  }

  void Update()
  {
    //float newAlpha = Mathf.Lerp(_timerTextTMP.color.a, 0.4f, 0.05f);
    //_timerTextTMP.color = new Color(0, 0, 0, newAlpha);
    if (_timeValue > 0) 
    {
      _timeValue -= Time.deltaTime;
    }
    else // no time left
    {
      _timeValue = 0;
      loseGame();
    }

    displayTime(_timeValue);
  }

  // ================== Helpers

  private void displayTime(float timeToDisplay)
  {
    timeToDisplay = Mathf.Max(0, timeToDisplay);
    // Calculating minutes and seconds
    int minutes = Mathf.FloorToInt(timeToDisplay / 60);
    int seconds = Mathf.FloorToInt(timeToDisplay % 60);
    numSeconds = Mathf.FloorToInt(timeToDisplay);
    _timerTextTMP.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    _strokeTextTMP.text = string.Format(projectileLauncher.GetComponent<BallLauncher>().numberStrokes.ToString());
  }  

  private void loseGame(){
    SceneManager.LoadScene("TestLose");
  }  
}
