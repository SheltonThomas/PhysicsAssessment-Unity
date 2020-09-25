using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PoolCueBehavior : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;
    private Rigidbody _controllerRigidBody;
    private List<GameObject> _poolCue;
    private Vector3 _gripToBody;
    private Vector3 _bodyToTip;

    public float poolCueMovementSpeed;
    public float poolCueRotationSpeed;
    public float poolCueDrawSpeed;
    public float maxDistance;
    public float hitSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _controllerRigidBody = _characterController.attachedRigidbody;
        _poolCue = new List<GameObject>();

        foreach (Transform child in transform)
        {
            _poolCue.Add(child.gameObject);
        }

        _gripToBody = _poolCue[0].transform.localPosition  - _poolCue[1].transform.localPosition;
        _bodyToTip = _poolCue[1].transform.localPosition - _poolCue[2].transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Movement(new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")), Input.GetAxis("SlowRotation") != 0, Input.GetAxis("SlowMovement") != 0);

        if (Input.GetAxis("Hit") != 0)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(transform.forward);
            Plane playerPlane = new Plane(Vector3.up, transform.position);

            float rayDistance = 0;

            playerPlane.Raycast(mouseRay, out rayDistance);

            Vector3 targetPoint = mouseRay.GetPoint(rayDistance);

            Vector3 fireDirection = targetPoint - transform.position;

            fireDirection.Normalize();

            Ray fireRay = new Ray(transform.position, fireDirection);

            RaycastHit objectHitInfo;
            if (Physics.Raycast(fireRay, out objectHitInfo))
            {
                if (objectHitInfo.collider.gameObject.tag == "PoolCue") return;
                Destroy(objectHitInfo.collider.gameObject);
            }
        }
    }

    private void Movement(Vector2 movement, bool slowRotation, bool slowMovement)
    {
        float movementSpeed = poolCueMovementSpeed;
        if (slowMovement) movementSpeed /= 2;
        _characterController.SimpleMove(transform.forward * movement.x * movementSpeed);

        float rotationSpeed = poolCueRotationSpeed;
        if (slowRotation) rotationSpeed /= 2;
        transform.Rotate(transform.up, movement.y * rotationSpeed * Time.deltaTime);
    }
}
