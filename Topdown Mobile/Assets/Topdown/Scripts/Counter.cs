using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public int count = 1;
    public StringTopic topic;

    public void Increment() {
        count++;
        topic.Raise(count.ToString());
    }
}
