using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// General Controller for the launcher
/// </summary>
public class LauncherController : MonoBehaviour
{

    [SerializeField]
    private Transform _launchPoint;

    [SerializeField]
    private BallLauncher launcher;

    [SerializeField]
    private Transform _teleportPoint; 

    [SerializeField]
    private float _currentRange = 10.0f;
    [SerializeField]
    private float _maxRange = 250.0f; 

    private Rigidbody _currentBall;

    public event System.Action OnLaunchAction;


    // Update is called once per frame
    void Update()
    {
        // Ensure launcher is visualizing correct path
        launcher.targetPosition = _launchPoint.position + (_launchPoint.forward * _currentRange);
        //launcher.visibleTrajectory = true; 
    }

    public void TeleportToPoint(Vector3 point)
    {
        Vector3 movement = point - _teleportPoint.position;
        transform.position += movement; 
    }

    public void AddRange(float toAdd)
    {
        _currentRange = Mathf.Clamp(_currentRange + toAdd, 0.01f, _maxRange);
    }


    public void SetTrajectoryVisible(bool set)
    {
        launcher.visibleTrajectory = set; 
    }

    public Rigidbody LaunchProjectile()
    {
        _currentBall = launcher.LaunchNewProjectile();

        OnLaunchAction.Invoke();

        return _currentBall;
    }


    public void RotateLauncher(float amount)
    {
        transform.Rotate(new Vector3(0, amount, 0));
    }

    
}
