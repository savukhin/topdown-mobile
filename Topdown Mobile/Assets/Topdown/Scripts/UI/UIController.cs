using UniRx;
using UnityEngine;
using UniRx.Triggers;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    public ProgressBar _hpBar;
    public RxJoystick fireJoystick;

    void Awake()
    {
        fireJoystick.OnDownAsObservable().Subscribe(_ => {
            WorldTimeSystem.Instance.StartAnim();
        });

        fireJoystick.OnMoveAsObservable()
            .Subscribe(x => {
                MessageBroker.Default.Publish(new PlayerFireMessage());
            });

        fireJoystick.OnUpAsObservable().Subscribe(_ => {
            WorldTimeSystem.Instance.StopAnim();
        });

        _hpBar.LinkToMax(_player.GetMaxHP());
        _hpBar.LinkToCurrent(_player.GetCurrentHP());
    }
}
