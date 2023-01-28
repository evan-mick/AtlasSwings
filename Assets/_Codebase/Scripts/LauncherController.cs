using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


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
    private AtlasAnimationController _controller; 

    [SerializeField]
    private float _currentRange = 10.0f;
    [SerializeField]
    private float _maxRange = 250.0f; 

    private Rigidbody _currentBall;

    public UnityEvent OnLaunchAction = new UnityEvent();


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

        _controller.ChangeAnimationState("NONE");
    }

    public void AddRange(float toAdd)
    {
        _currentRange = Mathf.Clamp(_currentRange + toAdd, 0.01f, _maxRange);
    }

    public void SetRange(float toSet)
    {
        _currentRange = Mathf.Clamp(toSet, 0.01f, _maxRange);
    }

    public void SetRangeByPercent(float percent)
    {
        _currentRange = Mathf.Clamp(percent * _maxRange, 0.01f, _maxRange);
    }


    public void SetTrajectoryVisible(bool set)
    {
        launcher.visibleTrajectory = set; 
    }

    public Rigidbody LaunchProjectile()
    {
        _currentBall = launcher.LaunchNewProjectile();

        OnLaunchAction.Invoke();

        SetAnimationFromVelocity(launcher.currentHeight);

        return _currentBall;
    }




    private void SetAnimationFromVelocity(float vel)
    {
        if (_controller != null)
        {
            if (vel > 15.0f)
            {
                _controller.ChangeAnimationState("HARD");
            }
            else if (vel > 12.0f)
            {
                _controller.ChangeAnimationState("LIGHT");
            }
            else if (vel > 8.0f)
            {
                _controller.ChangeAnimationState("MEDIUM");
            }
            else
            {
                _controller.ChangeAnimationState("HARD");
            }

        }
    }


    public void RotateLauncher(float amount)
    {
        transform.Rotate(new Vector3(0, amount, 0));
    }

    
}
