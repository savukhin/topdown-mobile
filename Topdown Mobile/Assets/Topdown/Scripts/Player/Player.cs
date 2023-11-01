using UnityEngine;
using UniRx;

public class Player : BasePlayer
{
    [SerializeField]
    private Joystick _joystick;

    private System.IDisposable fireSubsription;
    private System.IDisposable currentHPSubscription;

    void OnEnable() {
        fireSubsription = MessageBroker.Default.Receive<PlayerFireMessage>().Subscribe((PlayerFireMessage _) => { Fire(); });
        currentHPSubscription = _character.CurrentHp.Subscribe((hp) => {});
    }

    void OnDisable() {
        fireSubsription.Dispose();
        currentHPSubscription.Dispose();
    }

    void Update() {
        _character.SetSpeed(_joystick.Direction);
    }

    void Fire() {
        _character.Fire();
    }
}
