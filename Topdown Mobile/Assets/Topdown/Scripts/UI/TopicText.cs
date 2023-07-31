using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopicText : MonoBehaviour, IEventReceiver<string>
{
    public TMP_Text text;
    public StringTopic healthTopic;

    public UniqueId Id { get; } = new UniqueId();

    public void OnEvent(string ev) {
        text.text = ev;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        healthTopic.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
