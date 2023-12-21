using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    MeshRenderer meteorAppear;

    [Header("Meteor settings")]
    [SerializeField] float meteorAppearDelay = 3f;
    [SerializeField] float meteorDisappearDelay = 10f;
    [SerializeField] float meteorSpeedAxisX = -100f;
    [SerializeField] float meteorSpeedAxisY = -10f;

    [SerializeField] ParticleSystem meteorFlameOne;
    [SerializeField] ParticleSystem meteorFlameTwo;
    [SerializeField] ParticleSystem meteorFlameThree;

    // Start is called before the first frame update
    void Start()
    {
        meteorAppear = GetComponent<MeshRenderer>();
        meteorAppear.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > meteorAppearDelay && Time.time < meteorDisappearDelay)
        {
            meteorAppear.enabled = true;
            MoveMeteor();
            PlayParticlesMeteor();
            
        }

        else   // particli se ne nehajo playat, čeprav kličem metodo da se + če jo kličem jih sploh ni ?????????
        {
            meteorAppear.enabled = false;
            // StopParticlesMeteor();
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
