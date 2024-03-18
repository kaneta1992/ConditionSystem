using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConditionSystem
{
    public abstract class ConditionBase : ICondition, IDisposable
    {
        public bool IsMet
        {
            get => isMet;
            protected set
            {
                var lastIsMet = isMet;
                isMet = value;
                
                if (lastIsMet != isMet)
                {
                    OnMetChanged?.Invoke();
                }
            }
        }

        internal event Action OnMetChanged;

        internal ConditionManager parentConditionManager;

        internal int RefCount = 0;

        private bool isMet = false;

        public ConditionBase(ConditionManager parentConditionManager)
        {
            this.parentConditionManager = parentConditionManager;
            parentConditionManager.RegisterCondition(this);
        }
        
        
        public void Dispose()
        {
            parentConditionManager.UnregisterCondition(this);
        }
    }
}