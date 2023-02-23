using UnityEngine;
using UnityEngine.UI;
namespace Fishing
{
    public class Catcher : ItemLogicEx
    {
        private Transform m_Target;
        private float m_Speed, m_ClickDelayTime, m_OriginalValue = 0.1f;
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            CatcherParams catcherParams = userData as CatcherParams;
            ((RectTransform)transform).sizeDelta = new Vector2(catcherParams.CatcherScale, catcherParams.CatcherScale);
            m_Target = catcherParams.Target;
            m_Speed = catcherParams.Speed;
            GameEntry.Input.Register(InputSys.EnumInput.Mouse0, OnMouse0Down);
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            transform.position += m_Speed * Vector3.Lerp(Vector3.zero, m_Target.position - transform.position, realElapseSeconds);
            m_ClickDelayTime -= Time.deltaTime;
        }
        private void OnMouse0Down()
        {
            m_ClickDelayTime = m_OriginalValue;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Rock")
                m_Speed *= 0.2f;
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.tag == "Rock")
            {
                if (m_ClickDelayTime > 0)
                {
                    other.GetComponent<Rock>().GetDamage();
                }
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Rock")
                m_Speed *= 5f;
        }
    }
}