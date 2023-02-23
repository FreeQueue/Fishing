using UnityEngine;
using System;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
using GameFramework;
namespace Fishing.InputSys
{
    public enum EnumInput
    {
        /// <summary>
        /// F
        /// </summary>
        Interact,
        /// <summary>
        /// B
        /// </summary>
        Bag,
        /// <summary>
        /// Tab
        /// </summary>
        Helper,
        /// <summary>
        /// Esc
        /// </summary>
        Cancel,
        /// <summary>
        /// Q
        /// </summary>
        Question,
        /// <summary>
        /// 鼠标左键
        /// </summary>
        Mouse0,
    }
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Input")]
    public class InputComponent : GameFrameworkComponent
    {
        private InputEnvironment CurrentKeyEnvironment;
        private InputEnvironment DefaultAllKey;
        private Dictionary<EnumInput, ActionStack> m_ActionStackDic;
        private ActionStack m_AnyKeyAction;
        private (EnumInput, Action) m_TopKeyAction;
        private bool topFlag;
        public bool IsCanControl{
            get;
            set;
        }
        private Action<float> Horizontal, Vertical;
        private Action<bool> Run;
        protected override void Awake()
        {
            base.Awake();
            topFlag = false;
            IsCanControl = true;
            m_ActionStackDic = new Dictionary<EnumInput, ActionStack>();
            m_AnyKeyAction = new ActionStack();
            foreach (EnumInput enumInput in Enum.GetValues(typeof(EnumInput)))
            {
                m_ActionStackDic.Add(enumInput, new ActionStack());
            }
            DefaultAllKey=InputEnvironment.Create(Enum.GetValues(typeof(EnumInput)) as EnumInput[]);
            CurrentKeyEnvironment = DefaultAllKey;
        }
        public void Update()
        {
            if(IsCanControl)
            {
                if(Horizontal!=null)Horizontal.Invoke(Input.GetAxis("Horizontal"));
                if(Vertical!=null)Vertical.Invoke(Input.GetAxis("Vertical"));
                if(Run!=null)Run.Invoke(Input.GetButton("Run"));
            }
            if (topFlag)
            {
                if (Input.GetButtonDown(m_TopKeyAction.Item1.ToString()))
                {
                    m_TopKeyAction.Item2.Invoke();
                }
                return;
            }
            if (Input.anyKeyDown && !m_AnyKeyAction.IsEmpty())
            {
                m_AnyKeyAction.currentAction.Invoke();
                return;
            }
            foreach (var item in CurrentKeyEnvironment.m_EnumInputs)
            {
                if (Input.GetButtonDown(item.ToString())&&!m_ActionStackDic[item].IsEmpty())
                {
                    m_ActionStackDic[item].currentAction.Invoke();
                }
            }
        }
        public void RegisterHorizontal(Action<float> action)
        {
            Horizontal = action;
        }
        public void RegisterVertical(Action<float> action)
        {
            Vertical = action;
        }
        public void RegisterRun(Action<bool> action)
        {
            Run = action;
        }
        public void RegisterTopKey(EnumInput enumInput, Action action)
        {
            m_TopKeyAction.Item1 = enumInput;
            m_TopKeyAction.Item2 = action;
            topFlag = true;
        }
        public void UnregisterTopKey()
        {
            topFlag = false;
        }
        public void RegisterAnyKey(Action action)
        {
            m_AnyKeyAction.Register(action);
        }
        public void UnregisterAnyKey(Action action)
        {
            m_AnyKeyAction.Unregister(action);
        }
        public void Register(EnumInput enumInput, Action action)
        {
            m_ActionStackDic[enumInput].Register(action);
        }
        public void Unregister(EnumInput enumInput, Action action)
        {
            m_ActionStackDic[enumInput].Unregister(action);
        }
        public void SetInputEnvironment(params EnumInput[] enumInputs)
        {
            InputEnvironment inputEnvironment = InputEnvironment.Create(enumInputs);
            CurrentKeyEnvironment = inputEnvironment;
        }
        public void ClearInputEnvironment()
        {
            CurrentKeyEnvironment = DefaultAllKey;
        }
        private class InputEnvironment:IReference
        {
            public List<EnumInput> m_EnumInputs=new List<EnumInput>();
            public static InputEnvironment Create(params EnumInput[] enumInputs)
            {
                InputEnvironment inputEnvironment = ReferencePool.Acquire<InputEnvironment>();
                foreach(var enumInput in enumInputs)
                {
                    inputEnvironment. m_EnumInputs.Add(enumInput);
                }
                return inputEnvironment;
            }
            public void Clear()
            {
                m_EnumInputs.Clear();
            }
        }
        private class ActionStack
        {
            public Action currentAction
            {
                get
                {
                    return actions.Last.Value;
                }
            }
            private LinkedList<Action> actions = new LinkedList<Action>();
            public bool IsEmpty()
            {
                return actions.Count == 0;
            }
            public void Register(Action action)
            {
                if (actions.Contains(action)) actions.Remove(action);
                actions.AddLast(action);
            }
            public void Unregister(Action action)
            {
                actions.Remove(action);
            }
            public void Clear()
            {
                actions.Clear();
            }
        }
    }

}