using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BasePlayer
{
    [SerializeField]
    private float fireDelaySec = 1f;

    IEnumerator FireRate() {
        while (true) {
            _character.Fire();
            yield return new WaitForSeconds(fireDelaySec);
        }
    }

    void OnEnable() {
        StartCoroutine(FireRate());
    }

    void OnDisable() {
        StopCoroutine(FireRate());
    }
}
