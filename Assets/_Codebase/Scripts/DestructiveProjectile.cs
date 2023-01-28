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
    [SerializeField] private AudioSource bounceAudio;
    [SerializeField] private AudioClip bounceClip;


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

    public bool IsProjectileIneffectiveAndGrounded()
    {
        // Disable so we don't check self
        sphereCollider.enabled = false;

        bool grounded = Grounded();
        if (grounded)
            print("grounded");
            if (bounceAudio != null)
            {
            AudioSource.PlayClipAtPoint(bounceClip, transform.position);
            
            //bounceAudio.Play();
                //SoundManager.Instance.PlaySFX(bounceAudio);
            }
        // Check relatively still
        bool returnBool = rb.velocity.magnitude < setVelocityToZeroAmount && grounded;
        sphereCollider.enabled = true;

        return returnBool;
    }

    public bool Grounded()
    {
        
        return Physics.CheckSphere(rb.position + (Vector3.down * sphereCollider.radius * 1.1f * transform.localScale.y), sphereCollider.radius * 0.3f);
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
