using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WinScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _finalTimeTextTMP;
    [SerializeField] private TextMeshProUGUI _finalStrokesTextTMP;
    
    void Awake(){
        int strokes = PlayerPrefs.GetInt("strokes");
        int seconds = PlayerPrefs.GetInt("time");
        int min = Mathf.FloorToInt(seconds / 60);
        int sec = Mathf.FloorToInt(seconds % 60);
        _finalTimeTextTMP.text = string.Format("FINAL TIME REMAINING: {0:00}:{1:00}", min, sec);
        _finalStrokesTextTMP.text = string.Format("FINAL STROKE COUNT: " + strokes); 
    }    
}
