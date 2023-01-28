using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class SpawnLocations : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pooledObjects;
    [SerializeField] private GameObject player;

    void Awake()
    {
        int r = Random.Range(1,12);
        Debug.Log(r);
        player.transform.position = _pooledObjects[r].transform.position;
    }
        
}
