using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedEffectController : MonoBehaviour
{
    public GameObject player;
    private float speed;
    private Vector3 pos1, pos2;
    private  ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        pos1 = player.transform.localPosition;
        pos2 = pos1;
        particles = GetComponent<ParticleSystem>();
        var em = particles.emission;
        em.rateOverTime = 2f;
        speed = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        var em = particles.emission;
        pos2 = player.transform.localPosition;
        speed = 1f/Time.deltaTime*(pos2-pos1).magnitude;
        Debug.Log(speed);
       
        if (speed > 30f){
           em.rateOverTime = 10f;
          // Vector3 vel = rigidbody.velocity.normalized;
           //this.transform.localPosition = player.transform.localPosition + 1f*vel;
           //this.transform.LookAt(player.transform.localPosition);
        } 
        else {
           em.rateOverTime = 0f;
        }
        pos1 = pos2;
    }
}
