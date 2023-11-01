using UnityEngine;
using UniRx;


public class BasePlayer : MonoBehaviour
{
    [SerializeField]
    protected Character _character;

    public ReactiveProperty<int> GetCurrentHP() {
        return _character.CurrentHp;
    }

    public ReactiveProperty<int> GetMaxHP() {
        return _character.MaxHp;
    }
}
