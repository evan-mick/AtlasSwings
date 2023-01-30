using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    private float _bobMin = 40f;
    private float _bobMax = 60f;
    private float _bobSpeed = 1.5f;
    private float _bobY;

    // Update is called once per frame
    void Update()
    {
      float bobSin = Mathf.Sin(Time.time * _bobSpeed);
      _bobY = Mathf.Lerp(_bobMax, _bobMin, bobSin / 2.0f + 0.5f);
      this.transform.position = new Vector3(this.transform.position.x, _bobY, this.transform.position.z);
      this.transform.Rotate(0f, 1f, 0f, Space.Self);
    }
}
