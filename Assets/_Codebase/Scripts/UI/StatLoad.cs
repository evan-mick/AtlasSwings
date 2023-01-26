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
        _strokeTextTMP.text = this.transform.parent.GetComponent<ScoreTracker>().swings.ToString();
    }


    public void LoadTime(){
        float t = this.transform.parent.GetComponent<ScoreTracker>().timeElapsed;
        Debug.Log(t);
        int minutes = Mathf.FloorToInt(t / 60);
        int seconds = Mathf.FloorToInt(t % 60);
        _timerTextTMP.text = string.Format("{0:00}:{1:00}", minutes, seconds);


    }
}

        
