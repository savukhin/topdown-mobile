using UnityEngine;
using UniRx;

public class Player : BasePlayer
{
    [SerializeField]
    private Joystick _moveJoystick;

    [SerializeField]
    private Joystick _fireJoystick;

    private System.IDisposable fireSubsription;
    private System.IDisposable currentHPSubscription;

    void Awake() {
        _character.Tag.Value = CharacterTag.Player;
    }

    void OnEnable() {
        fireSubsription = MessageBroker.Default.Receive<PlayerFireMessage>().Subscribe((PlayerFireMessage _) => { Fire(); });
        currentHPSubscription = _character.CurrentHp.Subscribe((hp) => {});

    }

    void OnDisable() {
        fireSubsription.Dispose();
        currentHPSubscription.Dispose();
    }

    void Move() {
        #if UNITY_ANDROID 
            _character.SetSpeed(_moveJoystick.Direction);
        #else
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            _character.SetSpeed(new Vector2(x, y));
        #endif
    }

    void Targeting() {
        Vector3 dir = new (_fireJoystick.Direction.x, 0, _fireJoystick.Direction.y);
        Debug.DrawRay(_character.transform.position, dir, Color.red, 1, true);

        if (dir.magnitude > 0.1) {
            Enemy enemy = EnemyTargeterSystem.Instance.TargetEnemy(_character.transform.position, dir);

            if (enemy) {
                _character.SetTarget(enemy.gameObject);
            }
        }
    }

    void Update() {
        Move();
        Targeting();
    }

    void Fire() {
        _character.Fire();
    }
}
