using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera launchCamera;
    public CinemachineVirtualCamera projectileCamera;
    public CinemachineVirtualCamera mapCamera;
    public CinemachineVirtualCamera winCamera;


    public Transform projectileCameraRelative;


    [Header("Map Camera")]
    [SerializeField]
    private float _defaultMapCameraSize = 10.0f;
    [SerializeField]
    private float _maxMapCameraSize = 30.0f;
    [SerializeField]
    private float _minMapCameraSize = 5.0f;


    private Vector3 _startingProjectileCamLocalPosition;
    private Quaternion _startingProjectileCamLocalRotation;
    private Rigidbody followRb;

    [SerializeField]
    private GameObject _winLocationUIObject;
    [SerializeField]
    private GameObject _winLocation;


    private void Start()
    {

        projectileCamera.transform.SetParent(projectileCameraRelative);
        _startingProjectileCamLocalPosition = projectileCamera.transform.localPosition;
        _startingProjectileCamLocalRotation = projectileCamera.transform.localRotation;
        projectileCamera.transform.SetParent(null);

        SetLaunchCamera();
    }

    public void SetLaunchCamera()
    {
        DisableAllCameras();
        launchCamera.enabled = true; 
    }

    private void Update()
    {
        if (projectileCamera.LookAt != null && followRb != null)
        {
            //projectileCamera.transform.rotation.SetLookRotation(followRb.velocity.normalized);
        }

    }


    public void SetMapCamera(Vector3 lookOver)
    {
        mapCamera.transform.position = new Vector3(lookOver.x, 500, lookOver.z);
        SetMapCamera();
    }

    public void SetMapCamera()
    {
        DisableAllCameras();
        mapCamera.m_Lens.OrthographicSize = _defaultMapCameraSize;
        mapCamera.enabled = true;

        _winLocationUIObject.transform.parent = null;
        _winLocationUIObject.transform.position =
            new Vector3(_winLocation.transform.position.x, mapCamera.transform.position.y - 2.0f,
                _winLocation.transform.position.z);
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
        SetProjectileCamera();

        projectileCamera.Follow = follow.transform;
        projectileCamera.LookAt = follow.transform;

        followRb = projectileCamera.LookAt.GetComponent<Rigidbody>();

        //projectileCamera.transform.SetParent(follow.transform);
        //projectileCamera.transform.localPosition = _startingProjectileCamLocalPosition;
    }

    public void SetProjectileCamera()
    {
        DisableAllCameras();
        projectileCamera.enabled = true;
    }

    public void SetWinCamera()
    {
        DisableAllCameras();
        winCamera.enabled = true;
    }

    private void DisableAllCameras()
    {
        launchCamera.enabled = false;
        projectileCamera.enabled = false;
        mapCamera.enabled = false;
        winCamera.enabled = false;

        _winLocationUIObject.transform.parent = mapCamera.transform;


        projectileCamera.transform.SetParent(projectileCameraRelative);
        projectileCamera.transform.localPosition = _startingProjectileCamLocalPosition;
        projectileCamera.transform.localRotation = _startingProjectileCamLocalRotation;
        projectileCamera.transform.SetParent(null);
    }

    
}
