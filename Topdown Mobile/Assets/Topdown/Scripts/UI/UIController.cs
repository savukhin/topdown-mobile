using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    public ProgressBar _hpBar;
    public Button fireButton;

    void Awake()
    {
        fireButton.OnClickAsObservable().Subscribe(_ => {
            MessageBroker.Default.Publish(new PlayerFireMessage());
        });

        _hpBar.LinkToMax(_player.GetMaxHP());
        _hpBar.LinkToCurrent(_player.GetCurrentHP());
    }
}
