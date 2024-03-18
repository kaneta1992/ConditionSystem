using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConditionSystem
{
    internal class ConditionMetNotifier : IConditionObservable
    {
        internal ConditionManager parentConditionManager;

        private List<IConditionObserver> observers = new List<IConditionObserver>();

        internal ConditionBase TargetCondition;

        public ConditionMetNotifier(ConditionManager parentConditionManager, ConditionBase target)
        {
            this.parentConditionManager = parentConditionManager;
            TargetCondition = target;
            TargetCondition.OnMetChanged += Notify;
        }
        
        public void Subscribe(IConditionObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IConditionObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.OnConditionMetChanged(TargetCondition);
            }
        }
    }
}