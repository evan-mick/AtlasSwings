using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatLoad : MonoBehaviour
{
    [SerializeField] private Text _timerTextTMP;
    [SerializeField] private Text _strokeTextTMP;
    // Start is called before the first frame update
    public void LoadStrokes(){
        int s = PlayerPrefs.GetInt("strokes");
        _strokeTextTMP.text = string.Format(s.ToString());
    }


    public void LoadTime(){
        float t = PlayerPrefs.GetFloat("time");
        int minutes = Mathf.FloorToInt(t / 60);
        int seconds = Mathf.FloorToInt(t % 60);
        _timerTextTMP.text = string.Format("{0:00}:{1:00}", minutes, seconds);


    }
}

        
