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
        _rigidbody.velocity = transform.forward * _speed;

    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        var basePlayer = other.GetComponent<BasePlayer>();
        var character = other.GetComponent<Character>();
        Debug.Log("enemy" + enemy);
        Debug.Log("basePlayer" + basePlayer);
        Debug.Log("character" + character);
        Debug.Log("id " + other.name);
        Debug.Log("id2 " + other.gameObject.name);
        Debug.Log("this " + this.name);
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
