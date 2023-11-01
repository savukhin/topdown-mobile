using UnityEngine;

public abstract class IWeapon: MonoBehaviour {
    abstract public void OnPut(Character character);
    abstract public void Fire(Character character);
    abstract public WeaponType GetWeaponType();
}

public class PlayerFireMessage {}

public enum WeaponType {
    NoWeapon = 0,
    Pistol = 1,
}
