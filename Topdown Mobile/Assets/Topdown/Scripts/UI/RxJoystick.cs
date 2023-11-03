using System;
using UnityEngine;
using UniRx;

public class RxJoystick : MonoBehaviour
{
    [SerializeField]
    private Joystick _joystick;

    [SerializeField]
    private float _joystickThreshold = 0.01f;


    private Subject<Unit> _onDown = new Subject<Unit>();
    private Subject<Vector3> _onMove = new Subject<Vector3>();
    private Subject<Unit> _onUp = new Subject<Unit>();

    private bool _prevFrameJoystickDown = false;

    void Update() {
        Vector3 dir = _joystick.Direction;

        if (dir.magnitude < _joystickThreshold && !_prevFrameJoystickDown) {
            _prevFrameJoystickDown = true;
            _onUp.OnNext(Unit.Default);
        }

        if (dir.magnitude > _joystickThreshold && _prevFrameJoystickDown) {
            _prevFrameJoystickDown = false;
            _onDown.OnNext(Unit.Default);
        }

        if (dir.magnitude > _joystickThreshold) {
            _onMove.OnNext(dir);
        }
    }

    public IObservable<Unit> OnDownAsObservable() {
        return _onDown;
    }
    public IObservable<Vector3> OnMoveAsObservable() {
        return _onMove;
    }
    public IObservable<Unit> OnUpAsObservable() {
        return _onUp;
    }
}
