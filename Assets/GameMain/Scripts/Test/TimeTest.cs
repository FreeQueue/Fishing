using UnityEngine;
using Fishing;
using Fishing.Data;
public class TimeTest : MonoBehaviour{
    [ContextMenu("out strength")]
    public void OutStrength()
    {
        GameEntry.PlayerData.SetData(EnumIntData.Strength, 0);
    }
    [ContextMenu("up")]
    public void Get(){
        GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Bag).m_ItemGridGroupBase.AddItem(((int)EnumSpoil.重电金属Ⅱ型));
        GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Bag).m_ItemGridGroupBase.AddItem(((int)EnumSpoil.绿晶石));
    }
    [ContextMenu("up2")]
    public void Get2(){
        GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Bag).m_ItemGridGroupBase.AddItem(((int)EnumSpoil.具有强引力的金属块));
        GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Bag).m_ItemGridGroupBase.AddItem(((int)EnumSpoil.云海的设计手稿));
    }
}