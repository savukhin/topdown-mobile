using UnityEngine;

public class Pistol : IWeapon
{
    [SerializeField]
    private IBullet _bullet;

    [SerializeField]
    private Vector3 _firePoint;

    public override void Fire(Character character)
    {
        Debug.Log("Fire pistol");
        Instantiate(_bullet, getBulletStartPosition(), getBulletStartRotation());
    }

    public override WeaponType GetWeaponType()
    {
        return WeaponType.Pistol;
    }

    public override void OnPut(Character character)
    {
        character.animator.SetInteger("HasWeapon", (int)WeaponType.Pistol);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(getBulletStartPosition(), 0.05f);
    }

    private Vector3 getBulletStartPosition() {
        return transform.position + transform.rotation * _firePoint;
    }

    private Quaternion getBulletStartRotation() {
        Vector3 euler = transform.rotation.eulerAngles;
        euler.z = 0;
        euler.x = 0;
        return Quaternion.Euler(euler);
    }
}
