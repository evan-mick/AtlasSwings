using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class WinCondition : MonoBehaviour {
    private bool m_IsPlayerAtExit = false;
    public bool PlayerReachedGoal { get { return m_IsPlayerAtExit; } }

    public UnityEvent E_OnWinConditionMet = new UnityEvent();
    public ScoreTracker scoreInformation;
    public AudioSource winMusic;

    private Rigidbody _otherBody;

    //public static int strokes;
    //public static int seconds;

    void OnTriggerStay (Collider other)
    {
        if (other.tag == "Player")
        {
            if (_otherBody == null)
            {
                _otherBody = other.GetComponent<Rigidbody>();
            }
            //if (winMusic != null)
            //{
            //    print("hello?");
            //    SoundManager.Instance.PlayMusic(winMusic);
            //}

            if (_otherBody != null && _otherBody.velocity.magnitude < 1.0f)
            {

                m_IsPlayerAtExit = true;
                E_OnWinConditionMet.Invoke();
            }
        }
    }
}
