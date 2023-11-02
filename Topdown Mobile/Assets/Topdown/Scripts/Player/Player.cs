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
        #if UNITY_ANDROID 
            _character.SetSpeed(_joystick.Direction);
        #else
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            _character.SetSpeed(new Vector2(x, y));
        #endif
    }

    void Fire() {
        _character.Fire();
    }
}
