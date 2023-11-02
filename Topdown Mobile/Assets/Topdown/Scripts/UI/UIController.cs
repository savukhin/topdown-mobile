using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    public ProgressBar _hpBar;
    // public Button fireButton;
    public MyButton fireButton;

    void Awake()
    {
        fireButton.OnPointerDown().Subscribe(_ => {
            Debug.Log("Pointer down");
            WorldTimeSystem.Instance.StartAnim();
        });

        fireButton.OnPointerDown()
            .SelectMany(_ => this.UpdateAsObservable())
            .SampleFrame(1)
            .TakeUntil(fireButton.OnPointerUp())
            .RepeatUntilDestroy(this)
            .Subscribe(x => {
                MessageBroker.Default.Publish(new PlayerFireMessage());
            });

        fireButton.OnPointerUp().Subscribe(_ => {
            Debug.Log("Pointer up");
            WorldTimeSystem.Instance.StopAnim();
        });

        _hpBar.LinkToMax(_player.GetMaxHP());
        _hpBar.LinkToCurrent(_player.GetCurrentHP());
    }
}
