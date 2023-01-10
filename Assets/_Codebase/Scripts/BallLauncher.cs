using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blobcreate.ProjectileToolkit;

public class BallLauncher : MonoBehaviour
{

    public Rigidbody projectilePrefab;

    public Vector3 targetPosition; 

    [Range(-0.01f, -0.5f)]
    public float currentHeightCoefficient;

    [Header("Visuals")]
    public bool visibleTrajectory = true; 
    public TrajectoryPredictor trajectory;


    private void LateUpdate()
    {
        if (visibleTrajectory)
        {
            RenderLaunch(transform.position, targetPosition);
        }
    }

    public void RenderLaunch(Vector3 origin, Vector3 target)
    {
        var v = Projectile.VelocityByA(origin, target, currentHeightCoefficient);
        trajectory.Render(origin, v, target, 16);
    }

    public void LaunchNewProjectile()
    {

    }


    
}
