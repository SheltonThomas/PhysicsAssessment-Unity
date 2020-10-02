using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickBehavior : MonoBehaviour
{
    Rigidbody rb;
    MeshRenderer mr;

    bool isKinematic = false;

    public Material kinematicMat;
    public Material notKinematicMat;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (rb.isKinematic)
        {
            mr.material = kinematicMat;
        }
        if (!rb.isKinematic)
        {
            mr.material = notKinematicMat;
        }
    }

    private void OnMouseDown()
    {
        if(Camera.main.gameObject.GetComponent<CameraBehavior>().CurrentGimbal != CameraBehavior.Gimbal.sky)
        {
            return;
        }
        rb.isKinematic = !isKinematic;
        isKinematic = !isKinematic;
    }
}
