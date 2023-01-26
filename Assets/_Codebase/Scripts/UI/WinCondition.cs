using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class WinCondition : MonoBehaviour {
    private bool m_IsPlayerAtExit = false;
    public bool PlayerReachedGoal { get { return m_IsPlayerAtExit; } }

    public UnityEvent E_OnWinConditionMet = new UnityEvent();
    public ScoreTracker scoreInformation; 


    //public static int strokes;
    //public static int seconds;

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            m_IsPlayerAtExit = true;
            E_OnWinConditionMet.Invoke();
        }
    }
}
