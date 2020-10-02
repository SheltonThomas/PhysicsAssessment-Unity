using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCueShootBehavior : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Shoot") != 0)
        {
            Ray fireRay = new Ray(transform.position, transform.forward);
            if(Physics.Raycast(fireRay, out RaycastHit objectHitInfo))
            {
                Destroy(objectHitInfo.collider.gameObject);
            }
        }
    }
}
