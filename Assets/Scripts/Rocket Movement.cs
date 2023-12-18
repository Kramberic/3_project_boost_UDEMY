using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float liftThrust = 1f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    // [SerializeField] AudioClip sideEngines;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem sideEngineParticlesLeft;
    [SerializeField] ParticleSystem sideEngineParticlesRight;

    Rigidbody rocketPhysics;
    AudioSource engineSound;

    // Start is called before the first frame update --------------------------------------------------------------
    void Start()
    {
        rocketPhysics = GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame --------------------------------------------------------------
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // methods for movement --------------------------------------------------------------
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
        void ProcessRotation()
    {   
        if (Input.GetKey(KeyCode.LeftArrow))    // issue here is that left is prioritised over right since it has IF and the other has ELSE IF
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    // methods for thrusting --------------------------------------------------------------
    private void StartThrusting()
    {
        rocketPhysics.AddRelativeForce(Vector3.up * liftThrust * Time.deltaTime);
        
        if (!engineSound.isPlaying)
        {
            engineSound.clip = mainEngine;
            engineSound.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }
    private void StopThrusting()
    {
        engineSound.Stop();
        mainEngineParticles.Stop();
    }

    // methods for rotating --------------------------------------------------------------
    private void RotateLeft()
    {
        ShipRotation(rotationThrust);
        /*if (!engineSound.isPlaying)
        {
            engineSound.clip = sideEngines;
            engineSound.PlayOneShot(sideEngines);
        }*/
        if (!sideEngineParticlesRight.isPlaying)
        {
            sideEngineParticlesRight.Play();
        }
    }
    private void RotateRight()
    {
        ShipRotation(-rotationThrust);
        /*if (!engineSound.isPlaying)
        {
            engineSound.clip = sideEngines;
            engineSound.PlayOneShot(sideEngines);
        }*/
        if (!sideEngineParticlesLeft.isPlaying)
        {
            sideEngineParticlesLeft.Play();
        }
    }
    private void StopRotating()
    {
        // engineSound.Stop();
        sideEngineParticlesLeft.Stop();
        sideEngineParticlesRight.Stop();
    }

    void ShipRotation(float rotationThisFrame)
    {
        rocketPhysics.freezeRotation = true;   // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketPhysics.freezeRotation = false;  // unfreezing rotation so the physics system can take over
    }
}
