using UnityEngine;
using Fishing;
using Log=UnityGameFramework.Runtime.Log;
public class UITest : MonoBehaviour {
    [ContextMenu("tips")]
    public void OpenTestTips()
    {
        GameEntry.UI.OpenTipsForm("This is Test", TestCallback);
    }
    [ContextMenu("TipsPop")]
    public void OpenTestTipsPop()
    {
        GameEntry.UI.OpenTipsPopForm("This is Test");
    }
    [ContextMenu("MainGame")]
    public void OpenMainGame()
    {
        GameEntry.UI.OpenUIForm(EnumUIForm.UIMainGameForm);
    }
    [ContextMenu("Fishing")]
    public void OpenFishing()
    {
        GameEntry.UI.OpenUIForm(EnumUIForm.UIFishingForm);
    }
    private void TestCallback()
    {
        Log.Debug("Test UI Callback");
    }
}