using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeUpon : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    public void pschew()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject clone1 = Instantiate(explosion, player.transform.position, Quaternion.identity);
        clone1.transform.parent = player.transform;
    }
}
