using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedEffectController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rigidbody;
    private float count;
    ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        var em = particleSystem.emission;
        particleSystem = GetComponent<ParticleSystem>();
        rigidbody = player.GetComponent<Rigidbody>();
        em.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        var em = particleSystem.emission;
        if ((rigidbody.velocity).magnitude > 4.0f){
           em.enabled = true;
           Vector3 vel = rigidbody.velocity.normalized;
           this.transform.localPosition = player.transform.localPosition + 1f*vel;
           this.transform.LookAt(player.transform.localPosition);
        } 
        else {
            em.enabled = false;
        }
    }
}
