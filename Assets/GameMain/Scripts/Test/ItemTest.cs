using Fishing;
using UnityEngine;

public class ItemTest : MonoBehaviour
{
    [ContextMenu("spoil get Item")]
    public void SpoilGetItem()
    {
        GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Bag).m_ItemGridGroupBase.AddItem(((int)EnumSpoil.令人不安的黑匣子));

    }
    [ContextMenu("prop get Item")]
    public void PropGetItem()
    {
        GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Prop).m_ItemGridGroupBase.AddItem(((int)EnumSpoil.具有强引力的金属块));
    }
    [ContextMenu("openUI")]
    public void openUI()
    {
        GameEntry.UI.OpenUIForm(EnumUIForm.UIBagForm);
    }
    // [ContextMenu("spoil push Item")]
    // public void SpoilPushItem()
    // {
    //     GameEntry.ItemGrid.PushItem(((int)EnumSpoil.正方形), EnumGrid.Spoil, EnumGrid.Prop);
    // }
    // [ContextMenu("prop push Item")]
    // public void PropPushItem()
    // {
    //     GameEntry.ItemGrid.PushItem(((int)EnumSpoil.正方形), EnumGrid.Prop, EnumGrid.Spoil);
    // }
    [ContextMenu("spoil remove Item")]
    public void SpoilRemove()
    {
        GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Bag).m_ItemGridGroupBase.RemoveItemFromGrid(0);
    }
    [ContextMenu("save")]
    public void Save()
    {
        GameEntry.ItemGrid.Save();
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        foreach (var item in GameEntry.ItemGrid.GetAllItemGridGroupHelper())
        {
            item.m_ItemGridGroupBase.RemoveAllItem();
        }
    }
}
