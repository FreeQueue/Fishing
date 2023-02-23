using UnityEngine;
using GameFramework.Fsm;
namespace Fishing
{
    public class Normal : FsmState<MainController>
    {
        protected override void OnEnter(IFsm<MainController> fsm)
        {
            if (GameEntry.PlayerData.GetData(EnumBoolData.IsNewGame))
            {
                GameEntry.PlayerData.SetData(EnumBoolData.IsNewGame, false);
            }
            ChangeState<MainGame>(fsm);
        }
    }
}