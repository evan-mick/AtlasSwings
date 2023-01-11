using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FreezeFrameLogic : MonoBehaviour
{
    public static FreezeFrameLogic inst;
    public UnityEvent E_OnUnFreeze { get; private set; } = new UnityEvent();

    private void Awake()
    {
        if (inst == null)
        {
            inst = this; 
        } 
        else
        {
            Destroy(this);
        }
    }

    public void FreezeTime(float secs)
    {
        StartCoroutine(Freeze(secs));
    }

    private IEnumerator Freeze(float time)
    {
        float originalScale = Time.timeScale;
        Time.timeScale = 0.01f; 
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = originalScale;
        E_OnUnFreeze.Invoke();
        yield return null;
    }


}
