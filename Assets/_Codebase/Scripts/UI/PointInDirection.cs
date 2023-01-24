using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInDirection : MonoBehaviour
{
    Vector3 dir;
    float angle;
    public GameObject arrow;
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        dir = target.transform.position - transform.position;
        angle = Mathf.Atan2(dir.x, dir.z)*Mathf.Rad2Deg;
        arrow.transform.eulerAngles = new Vector3(90, 0, -angle);
    }
}
