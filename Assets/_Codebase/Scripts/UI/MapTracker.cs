using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTracker : MonoBehaviour
{
    [SerializeField] private Transform playerLocation;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        this.transform.position = playerLocation.position;
    }
}
