using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGimbalBehavior : MonoBehaviour
{
    public Transform target;

    public float speed;

    // Update is called once per frame
    void Update()
    {
        //transform.position = targetPosition;

        transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
        transform.forward = Vector3.Lerp(transform.forward, target.forward, speed * Time.deltaTime);
    }
}
