using UnityEngine;
using System.Collections;

public class WinCondition : MonoBehaviour
{
    public GameObject winObject; //can be transformed into "win scene" pretty easily

    void OnCollisionEnter(Collision collision)
    {
        winObject.SetActive(true);
    }
}