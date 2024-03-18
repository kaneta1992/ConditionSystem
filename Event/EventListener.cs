using System;
using System.Collections.Generic;
using UnityEngine;

namespace ConditionSystem
{
    public class EventListener : IConditionObserver, IDisposable
    {
        private bool IsMet;

        private int MetFrameCount;

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
            if (MetFrameCount == 0 && IsMet)
            {
                OnEnterMet();
            }

            if (MetFrameCount > 0 && IsMet)
            {
                OnStayMet();
            }

            if (MetFrameCount > 0 && !IsMet)
            {
                OnExitMet();
                MetFrameCount = 0;
            }

            if (IsMet)
            {
                MetFrameCount++;
            }
        }

        protected virtual void OnEnterMet()
        {
            Debug.Log("OnEnterMet");
        }

        protected virtual void OnStayMet()
        {
            Debug.Log("OnStayMet");
        }

        protected virtual void OnExitMet()
        {
            Debug.Log("OnExitMet");
        }

        public void OnConditionMetChanged(ICondition condition)
        {
            IsMet = condition.IsMet;
        }

        public void Dispose()
        {
            parentConditionManager.UnregisterEventListener(this);
        }
    }
}