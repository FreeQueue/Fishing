using UnityEngine;
using GameFramework.Event;
using GameFramework;
namespace Fishing
{
    public class PlayerDataChangeEventArgs:GameEventArgs
    {
        public static int EventId(EnumIntData enumData)
        {
            return enumData.ToString().GetHashCode();
        }
        public static int EventId(EnumBoolData enumData)
        {
            return enumData.ToString().GetHashCode();
        }
        public override int Id
        {
            get
            {
                return DataName.GetHashCode();
            }
        }
        public string DataName
        {
            get;
            private set;
        }
        public object Data
        {
            get;
            private set;
        }
        public object UserData
        {
            get;
            private set;
        }
        public static PlayerDataChangeEventArgs Create(string dataName,object data, object userData = null)
        {
            PlayerDataChangeEventArgs playerDataChangeEventArgs = ReferencePool.Acquire<PlayerDataChangeEventArgs>();
            playerDataChangeEventArgs.DataName = dataName;
            playerDataChangeEventArgs.Data = data;
            playerDataChangeEventArgs.UserData = userData;
            return playerDataChangeEventArgs;
        }

        public override void Clear()
        {
            DataName = null;
            Data = null;
            UserData = null;
        }
    }
}