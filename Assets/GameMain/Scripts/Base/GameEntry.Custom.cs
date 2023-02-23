using UnityEngine;
using UnityGameFramework.Runtime;

namespace Fishing
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static BuiltinDataComponent BuiltinData
        {
            get;
            private set;
        }

        public static ItemComponent Item
        {
            get;
            private set;
        }

        public static DataComponent Data
        {
            get;
            private set;
        }
        public static ItemGridGroupComponent ItemGrid
        {
            get;
            private set;
        }
        public static PlayerDataComponent PlayerData
        {
            get;
            private set;
        }
        public static InputSys.InputComponent Input
        {
            get;
            private set;
        }
        public static DialogComponent Dialog
        {
            get;
            private set;
        }
        public static TimeLineComponent TimeLine{
           get;
           private set;
        }
        private static void InitCustomComponents()
        {
            BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
            Item = UnityGameFramework.Runtime.GameEntry.GetComponent<ItemComponent>();
            Data = UnityGameFramework.Runtime.GameEntry.GetComponent<DataComponent>();
            GameEntry.Input = UnityGameFramework.Runtime.GameEntry.GetComponent<InputSys.InputComponent>();
            ItemGrid = UnityGameFramework.Runtime.GameEntry.GetComponent<ItemGridGroupComponent>();
            PlayerData = UnityGameFramework.Runtime.GameEntry.GetComponent<PlayerDataComponent>();
            Dialog = UnityGameFramework.Runtime.GameEntry.GetComponent<DialogComponent>();
            TimeLine =UnityGameFramework.Runtime.GameEntry.GetComponent<TimeLineComponent>();
        }
    }
}
