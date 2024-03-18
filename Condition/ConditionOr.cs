using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ConditionSystem
{
    public class ConditionOr : ConditionBase, IConditionObserver
    {
        private List<ConditionMetNotifier> conditionMetNotifiers = new List<ConditionMetNotifier>();

        public ConditionOr(ConditionManager parentConditionManager) : base(parentConditionManager)
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
            if (condition.IsMet)
            {
                IsMet = true;
                return;
            }

            IsMet = conditionMetNotifiers.Any(notifier => notifier.TargetCondition.IsMet);
        }
    }
}