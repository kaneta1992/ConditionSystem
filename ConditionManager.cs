using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ConditionSystem
{
    public class ConditionManager : IDisposable
    {
        private List<ConditionBase> Conditions = new List<ConditionBase>();

        private IEnumerable<PollingConditionBase> PollingConditions => Conditions.OfType<PollingConditionBase>();

        private List<EventListener> eventListners = new List<EventListener>();

        public ConditionManager()
        {
            ConditionSystemBrain.Instance.RegisterCondtionSystem(this);
        }

        public EventListener CreateEventListner(ConditionBase condition)
        {
            return new EventListener(this, condition);
        }

        internal void RegisterCondition(ConditionBase condition)
        {
            Conditions.Add(condition);
        }
        
        internal void UnregisterCondition(ConditionBase condition)
        {
            Conditions.Remove(condition);
        }
        
        internal void RegisterEventListener(EventListener eventListener)
        {
            eventListners.Add(eventListener);
        }
        
        internal void UnregisterEventListener(EventListener eventListener)
        {
            eventListners.Remove(eventListener);
        }

        internal void PollingUpdate(float deltaTime)
        {
            foreach (var condition in PollingConditions)
            {
                condition.PollingUpdate(deltaTime);
            }
        }

        internal void EventUpdate()
        {
            // TODO: 更新が必要なイベントリスナーのみ更新する（IsMet == trueの場合のみ）
            foreach (var eventListener in eventListners)
            {
                eventListener.EventUpdate();
            }
        }

        public void Dispose()
        {
            ConditionSystemBrain.Instance.UnregisterCondtionSystem(this);
        }
    }

}