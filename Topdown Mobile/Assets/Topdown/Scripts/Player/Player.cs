using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Character _character;

    [SerializeField]
    private Joystick _joystick;

    void Update() {
        _character.SetSpeed(_joystick.Direction);
    }    
}
