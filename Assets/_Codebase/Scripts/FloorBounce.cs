using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBounce : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private float lastBounce;
    void OnTriggerEnter(Collider other)
    {
        var currentBounce = Time.time;
        if (other.tag == "Player" && currentBounce - lastBounce > .2)
        {
            
            audioSource.Play();
        }
        lastBounce = currentBounce;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
