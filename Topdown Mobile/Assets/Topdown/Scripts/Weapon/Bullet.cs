using UnityEngine;

// [RequireComponent(typeof(CharacterController), typeof(Collider))]
public class Bullet : IBullet
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int damage = 10;
    private Rigidbody _rigidbody;

    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        _rigidbody.MovePosition(transform.position + transform.forward.normalized * _speed * WorldTimeSystem.GetDeltaTime());
    }

    void OnCollisionEnter(Collision collision)
    {
        Character character = collision.collider.GetComponent<Character>();
        Debug.Log("collide with " + collision.collider.name + " character = " + character);
        if (character != null) {
            character.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
