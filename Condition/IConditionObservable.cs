using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConditionSystem
{
    internal interface IConditionObservable
    {
        void Subscribe(IConditionObserver observer);
        
        void Unsubscribe(IConditionObserver observer);

        void Notify();
    }
}