using System;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private Character _character;

    [SerializeField]
    private Vector3 offset = new Vector3(0, 4, -4);

    [SerializeField]
    private float _speed = 1;

    [SerializeField]
    private float _height;
    
    void Start()
    {
        transform.position = _character.transform.position - offset;
        // transform.LookAt(_character.transform);
    }

    void Update()
    {
        Vector3 desiredPosition = _character.transform.position - offset;
        desiredPosition += transform.forward * 0.5f * WorldTimeSystem.GetTimeMultipler();
        transform.position = Vector3.Lerp(transform.position, desiredPosition, WorldTimeSystem.GetDeltaTime() * _speed);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + offset, 0.05f);
    }
}
