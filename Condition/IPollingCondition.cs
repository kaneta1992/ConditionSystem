using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConditionSystem
{
    public interface IPollingCondition : ICondition
    {
        void PollingUpdate(float deltaTime);
    }
}