using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ConditionSystem
{
    public class ConditionAnd : ConditionBase, IConditionObserver
    {
        private List<ConditionMetNotifier> conditionMetNotifiers = new List<ConditionMetNotifier>();

        public ConditionAnd(ConditionManager parentConditionManager) : base(parentConditionManager)
        {
        }

        public void AddCondition(ConditionBase condition)
        {
            var notifier = new ConditionMetNotifier(parentConditionManager, condition);
            notifier.Subscribe(this);
            conditionMetNotifiers.Add(notifier);
        }

        public void OnConditionMetChanged(ICondition condition)
        {
            if (!condition.IsMet)
            {
                IsMet = false;
                return;
            }

            IsMet = conditionMetNotifiers.All(notifier => notifier.TargetCondition.IsMet);
        }
    }
}