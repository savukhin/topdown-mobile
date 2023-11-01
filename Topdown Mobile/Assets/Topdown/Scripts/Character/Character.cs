using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unity.Profiling;
using System;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField]
    private Transform _placeForWeapon;
    public Animator animator { get; private set; }
    public new Rigidbody rigidbody { get; private set; }
    public float m_Speed = 5f;

    public ReactiveProperty<IWeapon> Weapon;
    public ReactiveProperty<int> CurrentHp;
    public ReactiveProperty<int> MaxHp;
    

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidbody = gameObject.GetComponent<Rigidbody>();

        Weapon.Subscribe((IWeapon newWeapon) => {
            if (newWeapon == null) {
                animator.SetInteger("HasWeapon", (int)WeaponType.NoWeapon);
            } else {
                animator.SetInteger("HasWeapon", (int)newWeapon.GetWeaponType());
            }
            
        });
    }

    public void Fire() {
        Weapon.Value.Fire(this);
    }

    public void SetSpeed(Vector2 direction) {
        Vector3 dir = new (direction.x, 0, direction.y);
        animator.SetFloat("Speed", dir.magnitude);

        rigidbody.MovePosition(transform.position + dir * Time.deltaTime * m_Speed);
        // _rigidbody.MoveRotation(transform.position + dir * Time.deltaTime * m_Speed);
        // transform.LookAt(transform, transform.position + dir);
        if (dir.magnitude > 0) {
            Quaternion desired_rot = Quaternion.LookRotation(dir);
            // Mathf.Lerp()
            Quaternion rot = Quaternion.Slerp(transform.rotation, desired_rot, 0.2f);
            rigidbody.MoveRotation( rot);
        }
    }

    public void TakeDamage(int damage) {
        CurrentHp.Value -= damage;
    }
}
 