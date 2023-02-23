using UnityEngine;
using UnityEngine.UI;
namespace Fishing
{
    public class Rock : ItemLogicEx
    {
        [SerializeField]
        private Image m_Image;
        [SerializeField]
        private CircleCollider2D m_Collider;
        public ItemImage RockImage
        {
            get => GameEntry.PlayerData.RockImage;
        }

        private int m_Health;
        private int Health
        {
            set
            {
                m_Health = value;
                if (m_Health < 1)
                {
                    
                    GameEntry.Sound.PlayMusic(EnumSound.水滴2);
                    GameEntry.Event.Fire(this, RockDestroyEventArgs.Create());
                }
            }
            get
            {
                return m_Health;
            }
        }
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            Health = (int)userData;
            m_Image.sprite =RockImage.GetImage((int)userData - 1);
            m_Image.SetNativeSize();
            m_Image.rectTransform.SetRectTransformSize(m_Image.rectTransform.rect.size / 3);
            m_Collider.radius = m_Image.rectTransform.rect.height/2;
        }
        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
        }
        public void GetDamage()
        {
            GameEntry.Sound.PlayMusic(EnumSound.水滴1);
            Health--;
        }
    }
}