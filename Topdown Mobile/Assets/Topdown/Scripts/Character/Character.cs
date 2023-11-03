using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
// using UniRx.Tuple;
using Unity.Profiling;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField]
    private Transform _placeForWeapon;
    public Animator animator { get; private set; }
    public new Rigidbody rigidbody { get; private set; }
    public float m_Speed = 5f;

    public ReactiveProperty<IWeapon> Weapon;
    public ReactiveProperty<int> CurrentHp = new ReactiveProperty<int>(100);
    public ReactiveProperty<int> MaxHp = new ReactiveProperty<int>(100);

    private ReactiveProperty<GameObject> _target = new ReactiveProperty<GameObject>(null);
    

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidbody = gameObject.GetComponent<Rigidbody>();

        _target.Subscribe(x => {
            if (x == null) return;

            x.transform
                .ObserveEveryValueChanged(x => x.position)
                .SelectMany(targetPosition => transform.ObserveEveryValueChanged(x => x.position)
                , (target, character) => System.Tuple.Create(target, character))
                .Subscribe((t) => {
                    Vector3 target = t.Item1;
                    Vector3 character = t.Item2;
                    Quaternion desired_rot = Quaternion.LookRotation(target - character);
                    Quaternion rot = Quaternion.Slerp(transform.rotation, desired_rot, 0.2f);
                    rigidbody.MoveRotation( rot);
                });
        });

        Weapon.Subscribe((IWeapon newWeapon) => {
            if (newWeapon == null) {
                animator.SetInteger("HasWeapon", (int)WeaponType.NoWeapon);
            } else {
                animator.SetInteger("HasWeapon", (int)newWeapon.GetWeaponType());
            }
            
        });
    }

    public void Fire() {
        Weapon.Value.Fire(this, _target.Value ? _target.Value.transform.position : transform.position + transform.forward);
    }

    public void SetSpeed(Vector2 direction) {
        Vector3 dir = new (direction.x, 0, direction.y);
        animator.SetFloat("Speed", dir.magnitude);

        rigidbody.MovePosition(transform.position + dir * m_Speed * WorldTimeSystem.GetDeltaTime());
        // _rigidbody.MoveRotation(transform.position + dir * Time.deltaTime * m_Speed);
        // transform.LookAt(transform, transform.position + dir);
        if (dir.magnitude > 0) {
            Quaternion desired_rot = Quaternion.LookRotation(dir);
            Quaternion rot = Quaternion.Slerp(transform.rotation, desired_rot, 0.2f);
            // rigidbody.MoveRotation( rot);
        }
    }

    public void SetTarget(GameObject target) {
        _target.Value = target;
    }

    public void TakeDamage(int damage) {
        CurrentHp.Value -= damage;
    }
}
 