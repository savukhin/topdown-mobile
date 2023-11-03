using UnityEngine;

public class Pistol : IWeapon
{
    [SerializeField]
    private IBullet _bullet;

    [SerializeField]
    private Vector3 _firePoint;

    [SerializeField]
    private float _fireDelaySec = 0.5f;

    private float desiredShotTime = 0;

    public override void Fire(Character character, Vector3 targetPosition)
    {
        if (Time.time < desiredShotTime) {
            return;
        }

        desiredShotTime = Time.time + _fireDelaySec;
        
        Instantiate(_bullet, getBulletStartPosition(), getBulletStartRotation(targetPosition));
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

    private Quaternion getBulletStartRotation(Vector3 targetPosition) {
        // Vector3 euler = transform.rotation.eulerAngles;
        // euler.z = 0;
        // euler.x = 0;
        // return Quaternion.Euler(euler);
        Vector3 startPos = getBulletStartPosition();
        Vector3 dir = targetPosition - startPos;
        dir.y = startPos.y;
        return Quaternion.LookRotation(dir);
    }
}
