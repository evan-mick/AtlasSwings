using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Destroyer : MonoBehaviour
{
    public UnityEvent E_OnDestroyObject = new UnityEvent();

    public float minimumDestructionVelocity = 10.0f; 

    private SphereCollider collider;
    private Rigidbody rb;

    private HashSet<DestroyIt.Destructible> _alreadyHit;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
        Debug.Assert(collider != null, "No Sphere Collider on Destroyer game object");

        rb = GetComponent<Rigidbody>();
        Debug.Assert(rb != null, "No RigidBody on Destroyer game object");

        _alreadyHit = new HashSet<DestroyIt.Destructible>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > minimumDestructionVelocity)
        {
            CheckAheadCollisions();
        }
    }

    private void CheckAheadCollisions()
    {
        collider.enabled = false;
        //Physics.CheckSphere()
        Collider[] collidedWith = Physics.OverlapSphere
            (rb.position + (rb.velocity * Time.fixedDeltaTime), collider.radius * 1.5f);// rb.gameObject.layer, QueryTriggerInteraction.Ignore);

        foreach (Collider collider in collidedWith)
        {
            ManageCollision(collider.gameObject);
        }
        collider.enabled = true; 
    }


    private void ManageCollision(GameObject collideWith)
    {
        if (rb.velocity.magnitude > minimumDestructionVelocity)
        {
            DestroyIt.Destructible destroyComponent = collideWith.GetComponent<DestroyIt.Destructible>();

            if (destroyComponent != null && !_alreadyHit.Contains(destroyComponent))
            {
                _alreadyHit.Add(destroyComponent);

                destroyComponent.ApplyDamage(rb.velocity.magnitude);

                if (destroyComponent.IsDestroyed)
                {
                    E_OnDestroyObject.Invoke();

                    FreezeFrameLogic.inst?.FreezeTime(.02f);
                }
            }
        }
    }


    /*private void OnCollisionEnter(Collision collision)
    {
        ManageCollision(collision.gameObject);
    }*/
}
