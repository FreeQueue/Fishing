using UnityEngine;
using Fishing;
using Fishing.Data;
public class DialogTest : MonoBehaviour{
    [ContextMenu("dialog 100")]
    public void StartDialog100()
    {
        GameEntry.Dialog.StartDialogGroup(100);
    }
    [ContextMenu("dialog 101")]
    public void StartDialog101()
    {
        GameEntry.Dialog.StartDialogGroup(101);
    }
    [ContextMenu("dialogData check")]
    public void DialogDataCheck()
    {
        DataDialogGroup dataDialogGroup= GameEntry.Data.GetData<DataDialogGroup>();
        var a= dataDialogGroup.GetDialogGroupData(100);
        foreach (var item in a)
        {
            Debug.Log(item.Dialog) ;
        }
    }
}