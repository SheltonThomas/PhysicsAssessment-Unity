using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField]
    private Transform regularGimbal;
    [SerializeField]
    private Transform skyGimbal;
    private Gimbal currentGimbal = 0;
    [SerializeField]
    private float lerpSpeed;

    private Vector3 distance;

    public enum Gimbal
    {
        regular,
        sky
    }

    private void Start()
    {
        currentGimbal = Gimbal.regular;
        distance = transform.position - regularGimbal.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 zeroVector = new Vector3(0, 0, 0);
        if (currentGimbal == Gimbal.regular && transform.localPosition != zeroVector)
        {
            transform.position = Vector3.Lerp(transform.position + distance, regularGimbal.position, lerpSpeed * Time.deltaTime);
            transform.forward = regularGimbal.forward;
        }
        if(currentGimbal == Gimbal.sky && transform.localPosition != zeroVector)
        {
            transform.position = Vector3.Lerp(transform.position, skyGimbal.position, lerpSpeed * Time.deltaTime);
            transform.forward = skyGimbal.forward;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentGimbal == Gimbal.regular)
            {
                currentGimbal++;
                transform.parent = skyGimbal;
            }
            else if (currentGimbal == Gimbal.sky)
            {
                currentGimbal--;
                transform.parent = regularGimbal;
            }
        }
    }

    public Gimbal CurrentGimbal => currentGimbal;
}
