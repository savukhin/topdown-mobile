using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : IBullet
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int damage = 10;
    private Rigidbody _rigidbody;

    private CharacterTag _characterTag = CharacterTag.Enemy;

    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        _rigidbody.MovePosition(transform.position + transform.forward.normalized * _speed * WorldTimeSystem.GetDeltaTime());
    }

    bool IsTakingDamage(CharacterTag other) {
        if (other == _characterTag)
            return false;
        if (other == CharacterTag.NPC)
            return false;

        return true;
    }

    void ProcessCollider(Collider collider) {
        Character character = collider.GetComponent<Character>();
        if (character != null && IsTakingDamage(character.Tag.Value)) {
            character.TakeDamage(damage);
        } else if (character != null) {
            return; // If we hit character which cannot be damaged we just skip
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider) {
       ProcessCollider(collider);
    }

    void OnCollisionEnter(Collision collision) {
        ProcessCollider(collision.collider);
    }

    public override void SetCharacterTag(CharacterTag tag)
    {
        _characterTag = tag;
    }
}
