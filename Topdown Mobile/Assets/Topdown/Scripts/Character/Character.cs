using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;
    public float m_Speed = 5f;

    void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    public void SetSpeed(Vector2 direction) {
        Vector3 dir = new (direction.x, 0, direction.y);
        _animator.SetFloat("Speed", dir.magnitude);

        _rigidbody.MovePosition(transform.position + dir * Time.deltaTime * m_Speed);
        // _rigidbody.MoveRotation(transform.position + dir * Time.deltaTime * m_Speed);
        // transform.LookAt(transform, transform.position + dir);
        if (dir.magnitude > 0) {
            Quaternion desired_rot = Quaternion.LookRotation(dir);
            // Mathf.Lerp()
            Quaternion rot = Quaternion.Slerp(transform.rotation, desired_rot, 0.2f);
            _rigidbody.MoveRotation( rot);
        }
    }
}
