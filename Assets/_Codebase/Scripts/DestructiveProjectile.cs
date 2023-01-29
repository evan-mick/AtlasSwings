using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Destroyer), typeof(SphereCollider))]
public class DestructiveProjectile : MonoBehaviour
{

    [SerializeField, Range(0.0f, 1.0f)]
    private float _onDestroySlowDownAmount = 0.5f;

    public float setVelocityToZeroAmount = 2.0f; 

    public UnityEvent E_OnStop = new UnityEvent();

    private Rigidbody rb;
    private Destroyer _destroyer;
    private SphereCollider sphereCollider;

    private float _timePassed;
    [SerializeField] private AudioClip bounceClip;

    private float lastBounce = 0f;
    private int rolls = 0;


    public bool Stopped { get; private set; } = false; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _destroyer = GetComponent<Destroyer>();
        sphereCollider = GetComponent<SphereCollider>();

        _destroyer.E_OnDestroyObject.AddListener(OnHitDestroyable);
    }

    private void FixedUpdate()
    {
        _timePassed += Time.fixedDeltaTime;

        if (_timePassed > 4.0f)
        {
            BounceBall();
        }

        if (_timePassed > 4.0f && !Stopped && IsProjectileIneffectiveAndGrounded())
        {
            rb.velocity = Vector3.zero;
            E_OnStop.Invoke();
            Stopped = true; 
        }   
    }

    public void AddForceWithDelay(float seconds, Vector3 force)
    {
        StartCoroutine(StartForce(seconds, force));
    }

    public IEnumerator StartForce(float seconds, Vector3 force)
    {

        yield return new WaitForSeconds(seconds);
        print("seconds passed");
        rb.AddForce(force, ForceMode.VelocityChange);
        yield return null; 
        //
    }

    public void BounceBall()
    {
        // Disable so we don't check self
        sphereCollider.enabled = false;
        bool bounced = Bounced();
        
        // Ball is rolling 
        if (bounced && rolls > 3)
        {
            // Play the sound less and less often as ball slows down
            if (Time.time - lastBounce > 2 / rb.velocity.magnitude)
            {
                if (bounceClip != null)
                {
                    SoundManager.Instance.PlaySFXClip(bounceClip);
                }
                lastBounce = Time.time;
            }
        }
        // Ball is bouncing
        else if (bounced && (Time.time - lastBounce > .2))
        {

            if (bounceClip != null)
            {
                SoundManager.Instance.PlaySFXClip(bounceClip);
            }

            lastBounce = Time.time;
            rolls = rolls + 1;
        }

        if (!bounced){rolls = 0;}
        sphereCollider.enabled = true;
    }

    public bool IsProjectileIneffectiveAndGrounded()
    {
        // Disable so we don't check self
        sphereCollider.enabled = false;

        bool grounded = Grounded();

        //if (grounded)
        //{
        //    print("grounded");
        //}

        

        // Check relatively still
        bool returnBool = rb.velocity.magnitude < setVelocityToZeroAmount && grounded;
        sphereCollider.enabled = true;

        return returnBool;
    }

    public bool Grounded()
    {
        return Physics.CheckSphere(rb.position + (Vector3.down * sphereCollider.radius * 1.1f * transform.localScale.y), sphereCollider.radius * 0.3f);
    }

    public bool Bounced()
    {
        var center = rb.position + (Vector3.down * sphereCollider.radius * 1.1f) ;
        var radius = sphereCollider.radius * 1.3f; // * 0.3f;

        return Physics.CheckSphere(center, radius);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(rb.position + (Vector3.down * sphereCollider.radius * 1.1f), sphereCollider.radius * 0.3f);
    }

    private void OnHitDestroyable()
    {
        rb.velocity *= _onDestroySlowDownAmount; 
    }
}
