using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using MEC;

public class WorldTimeSystem : MonoBehaviour
{
    static WorldTimeSystem _instance;
 
    public static WorldTimeSystem Instance
    {
        get
        {
            if (_instance == null) {
                Debug.LogError("WorldTimeSystem does not exists at scene, creating");
                GameObject go = new GameObject();
                _instance = go.AddComponent<WorldTimeSystem>();
            }
            return _instance;
        }
    }

    void Awake() {   
        _instance = this; 
    }


    [SerializeField]
    public ReactiveProperty<float> defaultTimeMultiplier { get; private set; } = new ReactiveProperty<float>(1);

    public ReactiveProperty<float> currentTimeMultiplier { get; private set; } = new ReactiveProperty<float>(1);

    [SerializeField]
    private AnimationCurve _multiplierCurve;

    private float _animStartTime;
    private CoroutineHandle _handler;
    private bool animStarted = false;

    private void OnEnable() {
        currentTimeMultiplier.Value = defaultTimeMultiplier.Value;
    }

    IEnumerator<float> AnimCoroutine(){
        for (;;)
        {
            currentTimeMultiplier.Value = _multiplierCurve.Evaluate(Time.time - _animStartTime);
            yield return Timing.WaitForOneFrame; 
        }
    }

    public void StartAnim() {
        if (animStarted) {
            return;
        }
        
        _animStartTime = Time.time;
        _handler = Timing.RunCoroutine(AnimCoroutine());
        animStarted = true;
    }

    public void StopAnim() {
        Timing.KillCoroutines(_handler);
        animStarted = false;
        currentTimeMultiplier.Value = defaultTimeMultiplier.Value;
    }

    public static float GetTimeMultipler() {
        return Instance.currentTimeMultiplier.Value;
    }

    public static float GetDeltaTime() {
        return Time.deltaTime * Instance.currentTimeMultiplier.Value;
    }
}
