using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ConditionSystem
{
    public class ConditionGroupDisposer : IDisposable
    {
        private List<ConditionBase> disposeTargets = new List<ConditionBase>();

        public ConditionGroupDisposer()
        {
        }

        public void Add(ConditionBase condition)
        {
            disposeTargets.Add(condition);
        }


        public void Dispose()
        {
            foreach (var target in disposeTargets)
            {
                target.Dispose();
            }
        }
    }
}