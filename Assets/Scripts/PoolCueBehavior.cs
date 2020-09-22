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
    private bool _canMoveBack = true;
    private bool _canMoveForward = false;
    private bool _isDrawing = false;
    private bool _isHitting = false;

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
        if(Input.GetAxis("Hit") != 0 && !_isHitting)
            Draw();
        else
            Hit();

        if (!_isHitting && !_isDrawing)
            Movement(new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")), Input.GetAxis("SlowRotation") != 0);

        if (_isDrawing)
            Movement(new Vector2(0, Input.GetAxis("Horizontal")), Input.GetAxis("SlowRotation") != 0);
    }

    private void Draw()
    {
        if (!_canMoveBack) return;

        _isDrawing = true;

        if (_poolCue[0].transform.localPosition.z <= -maxDistance)
        {
            _poolCue[0].transform.localPosition = new Vector3(0, 0, -maxDistance);
            _canMoveBack = false;
        }

        _poolCue[1].transform.localPosition = _poolCue[0].transform.localPosition - _gripToBody;
        _poolCue[2].transform.localPosition = _poolCue[1].transform.localPosition - _bodyToTip;

        foreach (GameObject cuePiece in _poolCue)
        {
            cuePiece.transform.position += -transform.forward * poolCueDrawSpeed * Time.deltaTime;
            _canMoveForward = true;
        }
    }

    private void Hit()
    {
        if (!_canMoveForward) return;

        _isDrawing = false;
        _isHitting = true;
        _canMoveBack = false;

        if (_poolCue[2].transform.localPosition.z >= 0)
        {
            _poolCue[2].transform.localPosition = new Vector3(0, 0, 0);
            _canMoveForward = false;
            _canMoveBack = true;
            _isHitting = false;
        }

        _poolCue[1].transform.localPosition = _poolCue[2].transform.localPosition + _bodyToTip;
        _poolCue[0].transform.localPosition = _poolCue[1].transform.localPosition + _gripToBody;

        foreach (GameObject cuePiece in _poolCue)
        {
            cuePiece.transform.position += transform.forward * hitSpeed * Time.deltaTime;
        }
    }

    private void Movement(Vector2 movement, bool slowRotation)
    {
        _characterController.SimpleMove(transform.forward * movement.x * poolCueMovementSpeed);
        float rotationSpeed = poolCueRotationSpeed;
        if (slowRotation)
        {
            rotationSpeed /= 2;
        }
        transform.Rotate(transform.up, movement.y * rotationSpeed * Time.deltaTime);
    }
}
