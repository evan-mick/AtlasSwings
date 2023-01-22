using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using Cinemachine;


public class CinemachineRotator : MonoBehaviour
{

    public float speed = 10f;

    private Cinemachine.CinemachineVirtualCamera m_VirtualCam;
    private Cinemachine.CinemachineOrbitalTransposer m_VirtualTransposer;

    void Start()
    {
        if (GetComponent<Cinemachine.CinemachineVirtualCamera>())
            m_VirtualCam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        m_VirtualTransposer = m_VirtualCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();


    }

    void Update()
    {
        if (m_VirtualTransposer != null)
            m_VirtualTransposer.m_XAxis.Value += Time.deltaTime * speed;
    }
}

