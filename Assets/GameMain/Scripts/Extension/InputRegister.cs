using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using System;
using System.Collections.Generic;
using Fishing.InputSys;
namespace Fishing
{
    public class InputRegister : IReference
    {
        private Dictionary<EnumInput,Action> m_ActionDic;

        public object Owner
        {
            get;
            private set;
        }

        public InputRegister()
        {
            m_ActionDic = new Dictionary<EnumInput, Action>();
            Owner = null;
        }

        public void Register(EnumInput enumInput, Action action)
        {
            if (action == null)
            {
                throw new Exception("Action is invalid.");
            }
            m_ActionDic.Add(enumInput,action);
            GameEntry.Input.Register(enumInput,action);
        }

        public void Unregister(EnumInput enumInput)
        {
            if(!m_ActionDic.ContainsKey(enumInput))return;
            GameEntry.Input.Unregister(enumInput, m_ActionDic[enumInput]);
            m_ActionDic.Remove(enumInput);        
        }
        public void UnRegisterAll()
        {
            if (m_ActionDic == null)return;
            foreach(var item in m_ActionDic)
            {
                GameEntry.Input.Unregister(item.Key, item.Value);
            }
            m_ActionDic.Clear();
        }
        public void SetActive(bool value)
        {
            if(value)
            {
                foreach (var item in m_ActionDic)
                {
                    GameEntry.Input.Register(item.Key, item.Value);
                }
            }
            else
            {
                foreach (var item in m_ActionDic)
                {
                    GameEntry.Input.Unregister(item.Key, item.Value);
                }
            }
        }
        public static InputRegister Create(object owner)
        {
            InputRegister inputRegister = ReferencePool.Acquire<InputRegister>();
            inputRegister.Owner = owner;
            return inputRegister;
        }

        public void Clear()
        {
            m_ActionDic.Clear();
            Owner = null;
        }
    }
}