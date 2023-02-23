using UnityEngine;

namespace Fishing
{
    public class Player : EntityLogicEx
    {
        [Header("Player")]
        [SerializeField]
        private float m_WalkSpeed = 1f;
        [SerializeField]
        private float m_TurnSpeed = 20f;
        Animator m_Animator;
        Rigidbody m_Rigidbody;
        float m_Horizontal, m_Vertical;
        Vector3 m_Movement;
        Quaternion m_Rotation = Quaternion.identity;
        private bool m_IsRunning;
        
        private Transform m_FollowedCamera;
        [Header("Camera")]
        public float sensitivityMouse = 2f;
        //观察距离
        public float Distance = 5F;
        //旋转角度
        private float mX = 0.0F;
        private float mY = 0.0F;
        //角度限制
        private float MiniLimitY = 5;
        private float MaxLimitY = 90;
        //是否启用插值
        public bool isNeedDamping = true;
        //速度
        public float Damping = 2.5F;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
        }
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            RegisterMove();
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            Move();
        }
        void LateUpdate()
        {
            //获取鼠标输入
            mX += Input.GetAxis("Mouse X") * sensitivityMouse * 0.02F;
            mY -= Input.GetAxis("Mouse Y") * sensitivityMouse * 0.02F;
            //范围限制
            mY = ClampAngle(mY, MiniLimitY, MaxLimitY);

            //重新计算位置和角度
            Quaternion mRotation = Quaternion.Euler(mY, mX, 0);
            Vector3 mPosition = mRotation * new Vector3(0.0F, 1.0F, -Distance) + transform.position;

            //设置相机角度和位置
            if (isNeedDamping)
            {
                //球形插值
                m_FollowedCamera.rotation = Quaternion.Slerp(m_FollowedCamera.rotation, mRotation, Time.deltaTime * Damping);
                //线性插值
                m_FollowedCamera.position = Vector3.Lerp(m_FollowedCamera.position, mPosition, Time.deltaTime * Damping);
            }
            else
            {
                m_FollowedCamera.rotation = mRotation;
                m_FollowedCamera.position = mPosition;
            }
        }
        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360) angle += 360;
            if (angle > 360) angle -= 360;
            return Mathf.Clamp(angle, min, max);
        }
        private void Move()
        {
            bool isWalking = !Mathf.Approximately(m_Horizontal, 0f) || !Mathf.Approximately(m_Vertical, 0f);
            if (GameEntry.Input.IsCanControl&&isWalking)
            {
                m_Movement.Set(m_Horizontal, 0f, m_Vertical);
                m_Movement.Normalize();//令这个向量长度为1
                m_Movement.Set
                    ((Quaternion.Euler(Quaternion.LookRotation(m_FollowedCamera.transform.forward).eulerAngles + Quaternion.LookRotation(m_Movement).eulerAngles) * Vector3.forward).normalized.x,
                    0f,
                     (Quaternion.Euler(Quaternion.LookRotation(m_FollowedCamera.transform.forward).eulerAngles + Quaternion.LookRotation(m_Movement).eulerAngles) * Vector3.forward).normalized.z);
                Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, m_TurnSpeed * Time.deltaTime, 0f);
                m_Rotation = Quaternion.LookRotation(desiredForward);
            }
            m_Animator.SetBool("IsWalking",isWalking);
            m_Animator.SetBool("IsRunning", m_IsRunning);
        }
        private void RegisterMove()
        {
            m_FollowedCamera = Camera.main.transform;
            GameEntry.Input.RegisterHorizontal(Horizontal);
            GameEntry.Input.RegisterVertical(Vertical);
            GameEntry.Input.RegisterRun(Run);
        }
        private void Horizontal(float value)
        {
            m_Horizontal = value;
        }
        private void Vertical(float value)
        {
            m_Vertical = value;
        }
        private void Run(bool value)
        {
            m_IsRunning = value;
        }
        void OnAnimatorMove()
        {
            m_Rigidbody.MovePosition(m_Rigidbody.position + m_WalkSpeed * m_Movement * m_Animator.deltaPosition.magnitude);
            m_Rigidbody.MoveRotation(m_Rotation);
        }
    }
}