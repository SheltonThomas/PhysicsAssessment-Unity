using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBallBehavior : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    public float hitForce;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRB = collision.collider.attachedRigidbody;

        otherRB.isKinematic = false;

        CharacterController controller = GetComponentInParent<CharacterController>();
        PoolCueBehavior poolCue = GetComponentInParent<PoolCueBehavior>();
        RagDollController ragDollController = collision.gameObject.GetComponentInParent<RagDollController>();

        if(ragDollController) ragDollController.isRagdollOn = true;

        float movementScale = controller.velocity.magnitude / poolCue.poolCueMovementSpeed;

        float forceToApply = hitForce * movementScale;

        otherRB.AddForce(transform.forward * forceToApply, ForceMode.Impulse);

        Debug.Log("Hit");
    }
}
