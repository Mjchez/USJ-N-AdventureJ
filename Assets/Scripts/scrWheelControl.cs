using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrWheelControl : MonoBehaviour
{
    public WheelCollider wheelCollider;
    public Transform wheelMesh;
    void Start()
    {
        
    }


    void FixedUpdate()
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelMesh.position = position;
        wheelMesh.rotation = rotation;
    }
}
