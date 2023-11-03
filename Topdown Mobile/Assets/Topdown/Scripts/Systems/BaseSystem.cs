
using System;
using UnityEngine;


public class BaseSystem<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;
 
    public static T Instance
    {
        get
        {
            if (_instance == null) {
                Debug.LogError(typeof(T) + "does not exists at scene, creating");
                GameObject go = new GameObject();
                _instance = go.AddComponent<T>();
            }
            return _instance;
        }
    }

    void Awake() {   
        // Debug.Log("Awake of system " + _instance + "type of" + typeof(T)); 
        // _instna
    }
}