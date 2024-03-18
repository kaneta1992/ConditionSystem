using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConditionSystem
{
    public abstract class PollingConditionBase : ConditionBase, IPollingCondition
    {
        private float IntervalSeconds { get; }

        private float TimeUntilNextUpdate { get; set; } = 0f;

        private float CurrentTime { get; set; } = 0f;

        public PollingConditionBase(ConditionManager parentConditionManager, float intervalSeconds) : base(parentConditionManager)
        {
            IntervalSeconds = intervalSeconds;
            CurrentTime = Time.realtimeSinceStartup;
            TimeUntilNextUpdate = Time.realtimeSinceStartup;
        }

        /// <summary>
        /// ポーリングの更新
        /// 内部的に指定した秒数感覚で処理を実行する
        /// </summary>
        /// <param name="deltaTime">経過時間</param>
        public void PollingUpdate(float deltaTime)
        {
            // ゲーム停止中に時間が進んでしまわないように加算方式にしている
            CurrentTime += deltaTime;

            while (TimeUntilNextUpdate <= CurrentTime)
            {
                Update(deltaTime);
                TimeUntilNextUpdate += IntervalSeconds;
            }
        }

        protected abstract void Update(float deltaTime);
    }
}