using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    // [SerializeField] [Range(0,1)] float movementFactor;   ----> have range adjustable from 0 to 1 in editor
    [SerializeField] float period = 2f;


    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }

        else
        {
            float cycles = Time.time / period;  // continually growing over time
            
            const float tau = Mathf.PI * 2;  // constant value of 6.238
            
            float rawSinWave = Mathf.Sin(cycles * tau);  // going from -1 to 1
            movementFactor = (rawSinWave + 1f) / 2f;  // recalculating to go from 0 to 1

            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;
        }
    }
}
