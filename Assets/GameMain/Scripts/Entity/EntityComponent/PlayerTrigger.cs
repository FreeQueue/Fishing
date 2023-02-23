using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Fishing
{
    public class PlayerTrigger : MonoBehaviour
    {
        private Action enterTrigger,exitTrigger;
        public bool IsActive{
            get;
            private set;
        }
        private void OnTriggerEnter(Collider other) {
            if(IsActive&&other.tag=="Player")
            {
                if(enterTrigger!=null)enterTrigger.Invoke();
            }
        }
        private void OnTriggerExit(Collider other) {
            if(IsActive&&other.tag=="Player")
            {
                if(exitTrigger!=null)exitTrigger.Invoke();
            }
        }
        public void Register(Action enterAction,Action exitAction)
        {
            enterTrigger += enterAction;
            exitTrigger += exitAction;
        }
        public void Unregister(Action enterAction,Action exitAction)
        {
            enterTrigger -= enterAction;
            exitTrigger -= exitAction;
        }
        public void Clear()
        {
            enterTrigger = null;
            exitTrigger = null;
        }
        public void SetActive(bool value)
        {
            IsActive = value;
        }
    }
}