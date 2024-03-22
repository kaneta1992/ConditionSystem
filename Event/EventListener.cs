using System;
using System.Collections.Generic;
using UnityEngine;

namespace ConditionSystem
{
    public class EventListener : IConditionObserver, IDisposable
    {
        public event Action OnEnterMet;
        public event Action OnStayMet;
        public event Action OnExitMet;

        public bool IsMet { get; private set; }

        private ConditionMetNotifier conditionMetNotifier;
        
        internal ConditionManager parentConditionManager;

        public EventListener(ConditionManager parentCondtionManager, ConditionBase condition)
        {
            parentConditionManager = parentCondtionManager;
            
            var notifier = new ConditionMetNotifier(parentCondtionManager, condition);
            notifier.Subscribe(this);
            conditionMetNotifier = (notifier);
            
            parentConditionManager.RegisterEventListener(this);
        }

        internal void EventUpdate()
        {
            if (IsMet)
            {
                OnStayMet?.Invoke();
            }
        }

        public void OnConditionMetChanged(ICondition condition)
        {
            var lastIsMet = IsMet;
            IsMet = condition.IsMet;
            
            if (!lastIsMet && IsMet)
            {
                OnEnterMet?.Invoke();
            }
            
            if (lastIsMet && !IsMet)
            {
                OnExitMet?.Invoke();
            }
        }

        public void Dispose()
        {
            parentConditionManager.UnregisterEventListener(this);
        }
    }
}