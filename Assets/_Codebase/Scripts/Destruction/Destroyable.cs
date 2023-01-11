using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destroyable : MonoBehaviour
{

    public UnityEvent E_OnDestroy = new UnityEvent();
    public bool destroyObjectOnDestroy = false;


    public void Destroy()
    {
        E_OnDestroy.Invoke();

        if (destroyObjectOnDestroy)
            Destroy(gameObject);
    }

}
