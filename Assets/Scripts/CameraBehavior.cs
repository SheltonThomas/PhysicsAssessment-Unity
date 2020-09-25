using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform target;
    [HideInInspector]
    public Vector3 offset;
    private Vector3 distance;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        distance = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + distance + offset;
        //transform.position = targetPosition;

        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        transform.forward = Vector3.Lerp(transform.forward, target.forward, speed * Time.deltaTime);
    }
}
