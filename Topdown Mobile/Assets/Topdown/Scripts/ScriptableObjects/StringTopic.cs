using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "String Topic", menuName = "Topics/String Topic", order = 1)]
public class StringTopic : BaseTopic<string>
{
    SpecificEventBus<string> _bus;
}
