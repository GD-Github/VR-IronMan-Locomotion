using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedEffectController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rigidbody;
    ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        main.startSpeed = 0.0f;
        rigidbody = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((rigidbody.velocity).magnitude > 2.0f){
            var main = particleSystem.main;
            main.startSpeed = 3.0f;
        } 
    }
}
