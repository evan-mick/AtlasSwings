using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blobcreate.ProjectileToolkit;
using Blobcreate.Universal;


/// <summary>
/// Responsible for launching new projectiles and updating their render path
/// </summary>
public class BallLauncher : MonoBehaviour
{

    public Rigidbody projectilePrefab;
    public int numberStrokes { get; private set; } = 0;
    public Vector3 targetPosition; 

    [Range(1.0f, 20.0f)]
    public float currentHeight;

    [Header("Visuals")]
    public bool visibleTrajectory = true; 
    public TrajectoryPredictor trajectory;


    private void LateUpdate()
    {
        if (visibleTrajectory)
        {
            trajectory.Line.forceRenderingOff = false;
            RenderLaunch(transform.position, targetPosition);
        }
        else
        {
            trajectory.Line.forceRenderingOff = true; 
        }
    }

    public void RenderLaunch(Vector3 origin, Vector3 target)
    {
        var v = Projectile.VelocityByHeight(origin, target, currentHeight);
        trajectory.Render(origin, v, target, 32);
    }

    public Rigidbody LaunchNewProjectile()
    {
        
        // COPIED FROM THE ASSET's "ProjectileLauncher" CLASS
        Rigidbody b = Instantiate(projectilePrefab, transform.position, transform.rotation);

        // Magic happens!
        Vector3 f = Projectile.VelocityByHeight(b.position, targetPosition, currentHeight);
        b.AddForce(f, ForceMode.VelocityChange);
        numberStrokes += 1;
        return b; 

        // Add some torque, not necessary, but interesting.
        /*var t = Vector3.Lerp(torqueForce * Random.onUnitSphere,
            torqueForce * (target - launchPoint.position).normalized, currentTorque);
        b.AddTorque(t, ForceMode.VelocityChange);*/
        
    }


    
}
