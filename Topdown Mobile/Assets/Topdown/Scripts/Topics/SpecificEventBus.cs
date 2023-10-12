using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public interface IBaseEventReceiver {
    public UniqueId Id { get; }
}

public interface IEventReceiver<T> : IBaseEventReceiver {
    void OnEvent(T ev);
}

public class SpecificEventBus<T>
{
    private readonly List<WeakReference<IEventReceiver<T>>> _receivers;
    private readonly Dictionary<string, WeakReference<IEventReceiver<T>>> _receiverHashToReference;


    public SpecificEventBus() {
        this._receivers = new List<WeakReference<IEventReceiver<T>>>();
        _receiverHashToReference = new Dictionary<string, WeakReference<IEventReceiver<T>>>();
    }

     public void Register(IEventReceiver<T> receiver)
    {
        if (!_receiverHashToReference.TryGetValue(receiver.Id, out WeakReference<IEventReceiver<T>> reference))
        {
            reference = new WeakReference<IEventReceiver<T>>(receiver);
            _receiverHashToReference[receiver.Id] = reference;
        }

        _receivers.Add(reference);
    }

    public void Unregister(IEventReceiver<T> receiver)
    {
        WeakReference<IEventReceiver<T>> reference = _receiverHashToReference[receiver.Id];

        _receivers.Remove(reference);

        int weakRefCount = _receivers.Count(x => x == reference);
        if (weakRefCount == 0)
            _receiverHashToReference.Remove(receiver.Id);
    }

    public void Raise(T msg) {
        foreach (var reference in _receivers)
        {
            if (reference.TryGetTarget(out IEventReceiver<T> receiver))
                receiver.OnEvent(msg);
        }
    }
}
