using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public ReactiveProperty<int> Maximum;
    public ReactiveProperty<int> Current;

    System.IDisposable _maximumSubscription;
    System.IDisposable _currentSubscription;

    [SerializeField]
    private Image mask;

    void OnEnable() {
        Subscribe();
    }

    void OnDisable() {
        Dispose();
    }

    void Subscribe() {
        _maximumSubscription = Maximum.Subscribe((_) => { Recalculate(); });
        _currentSubscription = Current.Subscribe((_) => { Recalculate(); });
    }

    void Dispose() {
        _maximumSubscription.Dispose();
        _currentSubscription.Dispose();
    }

    void Recalculate() {
        mask.fillAmount =(float)Current.Value / (float)Maximum.Value;
    }


    public void LinkToMax(ReactiveProperty<int> max) {
        Dispose();
        Maximum = max;
        Subscribe();
    }

    public void LinkToCurrent(ReactiveProperty<int> current) {
        Dispose();
        Current = current;
        Subscribe();
    }
}
