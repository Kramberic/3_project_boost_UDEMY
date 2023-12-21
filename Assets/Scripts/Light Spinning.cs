using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpinning : MonoBehaviour
{
    [SerializeField] float xAngle = 0f;
    [SerializeField] float yAngle = 10f;
    [SerializeField] float zAngle = 0f;

    public GameObject spinnyLight;
    void Update()
    {
        transform.Rotate(xAngle,yAngle,zAngle);
    }
}
