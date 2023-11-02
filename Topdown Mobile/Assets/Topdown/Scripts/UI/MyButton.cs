using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UIElements;
// using Unity.VisualScripting;

public class MyButton : MonoBehaviour
{
    private System.IObservable<PointerEventData> _onPointDown;
    private System.IObservable<PointerEventData> _onPointUp;
    private System.IObservable<PointerEventData> _onPointExit;
    private System.IObservable<PointerEventData> _onPointEnter;

    public IObservable<PointerEventData> OnPointerDown() {
        if (_onPointDown == null) {
            _onPointDown = gameObject
                .AddComponent<ObservablePointerDownTrigger>()
                .OnPointerDownAsObservable();
        }

        return _onPointDown;
    }

    public IObservable<PointerEventData> OnPointerUp() {
        if (_onPointUp == null) {
            _onPointUp = gameObject
                .AddComponent<ObservablePointerUpTrigger>()
                .OnPointerUpAsObservable();
        }

        return _onPointUp;
    }

    public IObservable<PointerEventData> OnPointerExit() {
        if (_onPointExit == null) {
            _onPointExit = gameObject
                .AddComponent<ObservablePointerExitTrigger>()
                .OnPointerExitAsObservable();
        }

        return _onPointExit;
    }
}
