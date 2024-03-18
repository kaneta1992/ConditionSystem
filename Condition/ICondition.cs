using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConditionSystem
{
    public interface ICondition
    {
        bool IsMet { get; }
    }
}