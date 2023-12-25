using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    MeshRenderer meteorAppear;

    [Header("Meteor settings")]
    [SerializeField] float meteorAppearDelay = 5f;
    [SerializeField] float meteorDisappearDelay = 20f;
    [SerializeField] float meteorSpeedAxisX = -100f;
    [SerializeField] float meteorSpeedAxisY = -10f;

    [SerializeField] ParticleSystem meteorFlameOne;
    [SerializeField] ParticleSystem meteorFlameTwo;
    [SerializeField] ParticleSystem meteorFlameThree;

    float timePlaying;

    void Start()
    {
        meteorAppear = GetComponent<MeshRenderer>();
        meteorAppear.enabled = false;
    }

    void Update()
    {
        timePlaying = Time.timeSinceLevelLoad;

        if (timePlaying < meteorAppearDelay)
        {
            StopParticlesMeteor();
        }
        
        else if (timePlaying >= meteorAppearDelay && timePlaying < meteorDisappearDelay)
        {
            meteorAppear.enabled = true;
            MoveMeteor();
            if(!meteorFlameOne.isPlaying && !meteorFlameTwo.isPlaying && !meteorFlameThree.isPlaying)
            {
                PlayParticlesMeteor();
            }
        }

        else
        {
            meteorAppear.enabled = false;
            StopParticlesMeteor();
        }
    }

    void MoveMeteor() // method for moving the meteor
    {
        float xValue = Time.deltaTime * meteorSpeedAxisX;
        float yValue = Time.deltaTime * meteorSpeedAxisY;
        transform.Translate(xValue,yValue,0);
    }

    void PlayParticlesMeteor() // method for playing meteor particles
    {
        meteorFlameOne.Play();
        meteorFlameTwo.Play();
        meteorFlameThree.Play();
    }

    void StopParticlesMeteor() // method for stopping meteor particles
    {
        meteorFlameOne.Stop();
        meteorFlameTwo.Stop();
        meteorFlameThree.Stop();
    }
}