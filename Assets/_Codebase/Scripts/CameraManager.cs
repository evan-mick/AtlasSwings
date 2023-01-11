using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera launchCamera;
    public CinemachineVirtualCamera projectileCamera;
    public CinemachineVirtualCamera mapCamera;


    [Header("Map Camera")]
    [SerializeField]
    private float _defaultMapCameraSize = 10.0f;
    [SerializeField]
    private float _maxMapCameraSize = 30.0f;
    [SerializeField]
    private float _minMapCameraSize = 5.0f;


    private Vector3 _startingProjectileCamLocalPosition; 


    private void Start()
    {
        _startingProjectileCamLocalPosition = projectileCamera.transform.localPosition;
        SetLaunchCamera();
    }

    public void SetLaunchCamera()
    {
        DisableAllCameras();
        launchCamera.enabled = true; 
    }

    public void SetMapCamera()
    {
        DisableAllCameras();
        mapCamera.m_Lens.OrthographicSize = _defaultMapCameraSize;
        mapCamera.enabled = true;
    }

    public void ResizeMapCamera(float amount)
    {
        mapCamera.m_Lens.OrthographicSize = Mathf.Clamp(mapCamera.m_Lens.OrthographicSize + amount, _minMapCameraSize, _maxMapCameraSize);
    }

    public void MoveMapCamera(Vector3 move)
    {
        mapCamera.transform.Translate(move);
    }


    public void SetProjectileCamera(Rigidbody follow)
    {
        projectileCamera.Follow = follow.transform;
        projectileCamera.LookAt = follow.transform;

        //projectileCamera.transform.SetParent(follow.transform);
        //projectileCamera.transform.localPosition = _startingProjectileCamLocalPosition;

        SetProjectileCamera();
    }

    public void SetProjectileCamera()
    {
        DisableAllCameras();
        projectileCamera.enabled = true;
    }

    private void DisableAllCameras()
    {
        launchCamera.enabled = false;
        projectileCamera.enabled = false;
        mapCamera.enabled = false;
    }
}
