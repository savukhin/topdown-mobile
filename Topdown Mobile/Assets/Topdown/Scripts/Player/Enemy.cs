using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Enemy : BasePlayer
{
    [SerializeField]
    private float _fireDelaySec = 2f;

    [SerializeField]
    private GameObject _spotlightGO;

    public ReactiveProperty<bool> Chosen = new ReactiveProperty<bool>(false);

    void Awake() {
        Chosen.Subscribe(x => {
            _spotlightGO.SetActive(x);
        });
    }

    IEnumerator FireRate() {
        while (true) {
            _character.Fire();
            yield return new WaitForSeconds(_fireDelaySec);
        }
    }

    void OnEnable() {
        StartCoroutine(FireRate());
    }

    void OnDisable() {
        StopCoroutine(FireRate());
    }

    public bool SetChosen(bool value) {
        bool prev = Chosen.Value;
        Chosen.Value = value;
        return prev;
    }
}
