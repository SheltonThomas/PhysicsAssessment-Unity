using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    public bool isRagdollOn
    {
        get
        {
            return !_animator.enabled;
        }
        set
        {
            _animator.enabled = !value;
        }
    }

    private void Start()
    {
        isRagdollOn = false;
    }
}
