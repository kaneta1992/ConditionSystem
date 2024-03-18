using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConditionSystem
{
    public class ConditionSystemBrain : MonoBehaviour
    {
        public static ConditionSystemBrain Instance
        {
            get
            {
                if (instance != null) return instance;

                instance = FindObjectOfType<ConditionSystemBrain>();

                if (instance != null) return instance;

                var singletonObject = new GameObject();
                instance = singletonObject.AddComponent<ConditionSystemBrain>();
                singletonObject.name = typeof(ConditionSystemBrain).ToString() + " (Singleton)";

                DontDestroyOnLoad(singletonObject);

                return instance;
            }
        }
        
        private static ConditionSystemBrain instance;

        private List<ConditionManager> conditionSystems = new List<ConditionManager>();

        internal void RegisterCondtionSystem(ConditionManager cs)
        {
            conditionSystems.Add(cs);
        }
        
        internal void UnregisterCondtionSystem(ConditionManager cs)
        {
            conditionSystems.Remove(cs);
        }
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this as ConditionSystemBrain;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        private void Update()
        {
            foreach (var conditionSystem in conditionSystems)
            {
                conditionSystem.PollingUpdate(Time.deltaTime);
            }
        }

        private void LateUpdate()
        {
            foreach (var conditionSystem in conditionSystems)
            {
                conditionSystem.EventUpdate();
            }
        }
    }

}