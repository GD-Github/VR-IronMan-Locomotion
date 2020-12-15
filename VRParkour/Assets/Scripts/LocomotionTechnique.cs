﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionTechnique : MonoBehaviour
{
    // Please implement your locomotion technique in this script. 
    public OVRInput.Controller leftController;
    public OVRInput.Controller rightController;
    public float forcePower;
    public GameObject hmd;
    public GameObject leftLaser, leftBeam, leftParticles;
    public GameObject rightLaser, rightBeam, rightParticles;
    [SerializeField] private float leftTriggerValue;    
    [SerializeField] private float rightTriggerValue;
    [SerializeField] private Vector3 leftPos, rightPos;
    [SerializeField] private Vector3 rightForce, leftForce;
    [SerializeField] private bool isIndexTriggerDown;
    private Rigidbody rb;
    private Quaternion leftControllerRot, rightControllerRot;
    private Vector3 leftDirection, rightDirection;

    /////////////////////////////////////////////////////////
    // These are for the game mechanism.
    public ParkourCounter parkourCounter;
    public string stage;
    
    void Start()
    {
        leftLaser.transform.localScale = Vector3.zero;
        leftBeam.transform.localScale = Vector3.zero;
        leftParticles.transform.localScale = Vector3.zero;
        rightLaser.transform.localScale = Vector3.zero;
        rightBeam.transform.localScale = Vector3.zero;
        rightParticles.transform.localScale = Vector3.zero;
        rb = this.GetComponent<Rigidbody>();
        forcePower = 0.25f;
    }

    void Update()
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // Please implement your LOCOMOTION TECHNIQUE in this script :D.
        transform.localEulerAngles = new Vector3(0.0f,transform.localEulerAngles.y ,0.0f);
        leftTriggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, leftController); 
        rightTriggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, rightController); 

        if (leftTriggerValue > 0.01f && rightTriggerValue > 0.01f)
        {
            if (!isIndexTriggerDown)
            {
                leftPos = OVRInput.GetLocalControllerPosition(leftController);
                rightPos = OVRInput.GetLocalControllerPosition(rightController);
                isIndexTriggerDown = true;
            }
            leftControllerRot = OVRInput.GetLocalControllerRotation(leftController);
            rightControllerRot = OVRInput.GetLocalControllerRotation(rightController);
            leftDirection = -1.0f*(leftControllerRot*Vector3.forward);
            rightDirection = -1.0f*(rightControllerRot*Vector3.forward);
        
            rightForce = rightTriggerValue*rightDirection ;
            leftForce =  leftTriggerValue*leftDirection;

            rb.AddForce(forcePower*rightForce,ForceMode.Impulse);
            rb.AddForce(forcePower*leftForce,ForceMode.Impulse);

          //  Debug.DrawRay((leftPos + rightPos)/2, offset, Color.red, 0.2f);
        }
        else if (leftTriggerValue > 0.01f && rightTriggerValue < 0.01f)
        {
            if (!isIndexTriggerDown)
            {
                isIndexTriggerDown = true;
                leftPos = OVRInput.GetLocalControllerPosition(leftController);
            }
            leftControllerRot = OVRInput.GetLocalControllerRotation(leftController);
            leftDirection = -1.0f*(leftControllerRot*Vector3.forward);

            leftForce = (leftTriggerValue*leftDirection);

            rb.AddForce(forcePower*leftForce,ForceMode.Impulse);

         //   Debug.DrawRay(leftPos, offset, Color.red, 0.2f);
        }
        else if (leftTriggerValue < 0.01f && rightTriggerValue > 0.01f)
        {
            if (!isIndexTriggerDown)
            {
                isIndexTriggerDown = true;
                rightPos = OVRInput.GetLocalControllerPosition(rightController);
            }
            rightControllerRot = OVRInput.GetLocalControllerRotation(rightController);
            rightDirection = -1.0f*(rightControllerRot*Vector3.forward);

            rightForce = (rightTriggerValue*rightDirection);

            rb.AddForce(forcePower*rightForce,ForceMode.Impulse);
            
          //  Debug.DrawRay(rightPos, offset, Color.red, 0.2f);
        }
        else
        {
            if (isIndexTriggerDown)
            {
                leftLaser.transform.localScale = Vector3.zero;
                leftBeam.transform.localScale = Vector3.zero;
                leftParticles.transform.localScale = Vector3.zero;
                rightLaser.transform.localScale = Vector3.zero;
                rightBeam.transform.localScale = Vector3.zero;
                rightParticles.transform.localScale = Vector3.zero;
                isIndexTriggerDown = false;
                rightForce = Vector3.zero;
                leftForce = Vector3.zero;
            }
        }

        if (leftTriggerValue> 0.01f && leftTriggerValue <0.20f){
            leftBeam.transform.localScale = 5*leftTriggerValue*Vector3.one;
            leftLaser.transform.localScale = Vector3.zero;
            leftParticles.transform.localScale = Vector3.zero;
        }

        if (rightTriggerValue> 0.01f && rightTriggerValue <0.20f){
            rightBeam.transform.localScale = 5*rightTriggerValue*Vector3.one;
            rightLaser.transform.localScale = Vector3.zero;
            rightParticles.transform.localScale = Vector3.zero;
        }

        if (leftTriggerValue>= 0.20f && leftTriggerValue <0.80f){
            leftBeam.transform.localScale = leftTriggerValue*Vector3.one;
            leftLaser.transform.localScale = leftTriggerValue*Vector3.one;
            leftParticles.transform.localScale = Vector3.zero;

        }
        if (leftTriggerValue>= 0.80f){
            leftBeam.transform.localScale = Vector3.one;
            leftLaser.transform.localScale = leftTriggerValue*Vector3.one;
            leftParticles.transform.localScale = leftTriggerValue*Vector3.one;
        }

        if (rightTriggerValue>= 0.20f && rightTriggerValue <0.80f){
            rightBeam.transform.localScale = rightTriggerValue*Vector3.one;
            rightLaser.transform.localScale = rightTriggerValue*Vector3.one;
            rightParticles.transform.localScale = Vector3.zero;

        }
        if (rightTriggerValue>= 0.80f){
            rightBeam.transform.localScale = Vector3.one;
            rightLaser.transform.localScale = rightTriggerValue*Vector3.one;
            rightParticles.transform.localScale = rightTriggerValue*Vector3.one;
        }

        ////////////////////////////////////////////////////////////////////////////////
        // These are for the game mechanism.
        if (OVRInput.Get(OVRInput.Button.Two) || OVRInput.Get(OVRInput.Button.Four))
        {
            if (parkourCounter.parkourStart)
            {
                this.transform.position = parkourCounter.currentRespawnPos;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        // These are for the game mechanism.
        if (other.CompareTag("banner"))
        {
            stage = other.gameObject.name;
            parkourCounter.isStageChange = true;
        }
        else if (other.CompareTag("coin"))
        {
            parkourCounter.coinCount += 1;
            this.GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
        }
        // These are for the game mechanism.

    }
    void OnCollisionEnter(Collision other){
        rb.angularVelocity = Vector3.zero;
    }

    void OnCollisionStay(Collision other){
        rb.angularVelocity = Vector3.zero;
    }
}